using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.AdminFeatures.AdminMatches.DTOs;

namespace TipsaNu.Application.AdminFeatures.AdminMatches.Queries.GetFilteredCompetitors;

public record GetFilteredCompetitorsQuery(
    int TournamentId, 
    int? GroupId = null, 
    string? SearchTerm = null
) : IRequest<OperationResult<List<CompetitorDto>>>;