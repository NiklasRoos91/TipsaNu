using Moq;
using TipsaNu.Application.AdminFeatures.AdminMatches.Commands.CreateMatch;
using TipsaNu.Application.AdminFeatures.AdminMatches.DTOs;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Enums;
using TipsaNu.Domain.Interfaces;
using Match = TipsaNu.Domain.Entities.Match;

namespace TipsaNu.Test.Application.AdminFeatures.AdminMatch;

public class CreateMatchCommandHandlerTests
{
    private readonly Mock<IGenericRepository<Match>> _genericRepoMock;
    private readonly Mock<IMatchRepository> _matchRepoMock;
    private readonly CreateMatchCommandHandler _handler;

    public CreateMatchCommandHandlerTests()
    {
        _genericRepoMock = new Mock<IGenericRepository<Match>>();
        _matchRepoMock = new Mock<IMatchRepository>();
        _handler = new CreateMatchCommandHandler(
            _genericRepoMock.Object, 
            _matchRepoMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateMatchAndReturnDto_WithCorrectDeadline()
    {
        // Arrange
        var startTime = DateTime.Now.AddDays(7);
        var createDto = new CreateMatchDto 
        {
            TournamentId = 1,
            HomeCompetitorId = 10,
            AwayCompetitorId = 20,
            MatchType = MatchTypeEnum.Group,
            RoundNumber = 1,
            GroupId = 5,
            StartTime = startTime,
            PredictionDeadline = null
        };

        var command = new CreateMatchCommand(createDto);

        var matchFromDb = new Match 
        { 
            MatchId = 99, 
            HomeCompetitor = new Competitor { Name = "Norway" },
            AwayCompetitor = new Competitor { Name = "Denmark" },
            StartTime = startTime,
            PredictionDeadline = startTime, 
            Status = MatchStatusEnum.Scheduled
        };

        _matchRepoMock.Setup(x => x.GetMatchWithCompetitorsAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(matchFromDb);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal("Norway", result.Data.HomeCompetitorName);
        Assert.Equal(startTime, result.Data.PredictionDeadline);
        
        _genericRepoMock.Verify(x => x.AddAsync(It.IsAny<Match>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnFailure_IfMatchCannotBeFoundAfterSave()
    {
        // Arrange
        _matchRepoMock.Setup(x => x.GetMatchWithCompetitorsAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Match)null!);

        var command = new CreateMatchCommand(new CreateMatchDto());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Match was created but could not be retrieved from the database.", result.ErrorMessage);
    }
}