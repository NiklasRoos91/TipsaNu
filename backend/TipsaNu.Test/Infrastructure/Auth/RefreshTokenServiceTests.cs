using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Enums;
using TipsaNu.Infrastructure.Auth;
using TipsaNu.Infrastructure.Persistence;

namespace TipsaNu.Test.Infrastructure.Auth;

public class RefreshTokenServiceTests : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly AppDbContext _db;
    private readonly RefreshTokenService _service;

    public RefreshTokenServiceTests()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .Options;

        _db = new AppDbContext(options);
        _db.Database.EnsureCreated();

        _service = new RefreshTokenService(_db);
    }

    public void Dispose()
    {
        _db.Dispose();
        _connection.Dispose();
    }

    private async Task<User> SeedUserAsync()
    {
        var user = new User
        {
            UserName = "tester",
            Email = "tester@example.com",
            PasswordHash = "hash",
            CreatedAt = DateTime.UtcNow,
            Role = UserRoleEnum.User
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    [Fact]
    public async Task RotateRefreshTokenAsync_ShouldRevokeOldToken_AndCreateNewActiveToken()
    {
        // Arrange
        var user = await SeedUserAsync();
        var oldToken = await _service.CreateRefreshTokenAsync(user);

        // Act
        var newToken = await _service.RotateRefreshTokenAsync(oldToken);

        // Assert
        var oldFromDb = await _db.RefreshTokens
            .AsNoTracking()
            .FirstAsync(rt => rt.RefreshTokenId == oldToken.RefreshTokenId);
        var newFromDb = await _db.RefreshTokens
            .AsNoTracking()
            .FirstAsync(rt => rt.RefreshTokenId == newToken.RefreshTokenId);

        Assert.True(oldFromDb.Revoked);
        Assert.False(newFromDb.Revoked);
        Assert.NotEqual(oldFromDb.Token, newFromDb.Token);
        Assert.Equal(user.UserId, newFromDb.UserId);
        Assert.True(newFromDb.ExpiresAt > DateTime.UtcNow);
    }

    [Fact]
    public async Task RotateRefreshTokenAsync_ShouldKeepOldTokenRow_InsteadOfDeletingIt()
    {
        var user = await SeedUserAsync();
        var oldToken = await _service.CreateRefreshTokenAsync(user);

        await _service.RotateRefreshTokenAsync(oldToken);

        var count = await _db.RefreshTokens.CountAsync(rt => rt.RefreshTokenId == oldToken.RefreshTokenId);
        Assert.Equal(1, count);
    }

    [Fact]
    public async Task RotateRefreshTokenAsync_OldTokenValue_ShouldNoLongerBeRetrievableAsValid()
    {
        var user = await SeedUserAsync();
        var oldToken = await _service.CreateRefreshTokenAsync(user);
        var oldTokenValue = oldToken.Token;

        await _service.RotateRefreshTokenAsync(oldToken);

        var result = await _service.GetRefreshTokenAsync(oldTokenValue);
        Assert.Null(result);
    }

    [Fact]
    public async Task RotateRefreshTokenAsync_NewToken_ShouldBeRetrievableAsValid()
    {
        var user = await SeedUserAsync();
        var oldToken = await _service.CreateRefreshTokenAsync(user);

        var newToken = await _service.RotateRefreshTokenAsync(oldToken);

        var result = await _service.GetRefreshTokenAsync(newToken.Token);
        Assert.NotNull(result);
        Assert.Equal(newToken.RefreshTokenId, result!.RefreshTokenId);
    }
}
