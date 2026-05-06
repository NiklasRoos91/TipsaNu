using MediatR;
using TipsaNu.Application.Commons.Interfaces;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Leagues.DTOs;
using TipsaNu.Application.Features.Leagues.Mappers;
using TipsaNu.Application.Services.Interfaces;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Leagues.Commands.CreateLeague
{
    public class CreateLeagueCommandHandler(
        ILeagueRepository leagueRepository,
        IGenericRepository<League> genericLeagueRepository,
        IGenericRepository<Tournament> genericTournamentRepository,
        ILeagueMemberService leagueMemberService,
        ICurrentUserService currentUser)
        : IRequestHandler<CreateLeagueCommand, OperationResult<CreatedLeagueWithMemberDto>>
    {
        public async Task<OperationResult<CreatedLeagueWithMemberDto>> Handle(CreateLeagueCommand request, CancellationToken cancellationToken)
        {
            var userId = currentUser.UserId;
            if (userId <= 0)
                return OperationResult<CreatedLeagueWithMemberDto>.Failure("Unauthorized");

            var tournament = await genericTournamentRepository.GetByIdAsync(request.LeagueDto.TournamentId, cancellationToken);
            if (tournament == null)
                return OperationResult<CreatedLeagueWithMemberDto>.Failure("Tournament not found");

            // Generate invitation code if null
            var invitationCode = string.IsNullOrWhiteSpace(request.LeagueDto.InvitationCode)
                ? Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper()
                : request.LeagueDto.InvitationCode;

            var codeExists = await leagueRepository.InvitationCodeExistsAsync(request.LeagueDto.TournamentId, invitationCode, cancellationToken);
            if (codeExists)
            {
                return OperationResult<CreatedLeagueWithMemberDto>.Failure("Invitation code already exists. Please try again or leave empty to auto-generate.");
            }

            var league = new League
            {
                Name = request.LeagueDto.Name,
                Description = request.LeagueDto.Description,
                AdminUserId = userId,
                TournamentId = request.LeagueDto.TournamentId,
                InvitationCode = invitationCode,
                CreatedAt = DateTime.UtcNow,
                MaxMembers = request.LeagueDto.MaxMembers
            };

            await genericLeagueRepository.AddAsync(league, cancellationToken);

            var leagueDto = league.ToLeagueDto();
            var memberDto = await leagueMemberService.AddMemberWithLeaderboardAsync(league.LeagueId, userId, cancellationToken);

            var createdLeagueWithMemberDto = new CreatedLeagueWithMemberDto
            {
                LeagueDto = leagueDto,
                LeagueMemberDto = memberDto
            };

            return OperationResult<CreatedLeagueWithMemberDto>.Success(createdLeagueWithMemberDto);
        }
    }
}