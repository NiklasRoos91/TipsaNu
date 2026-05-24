using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.AdminFeatures.AdminMatches.DTOs;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.AdminFeatures.AdminMatches.Queries.GetFilteredCompetitors;

public class GetFilteredCompetitorsQueryHandler(IGenericRepository<Competitor> genericCompetitorRepository) 
    : IRequestHandler<GetFilteredCompetitorsQuery, OperationResult<List<CompetitorDto>>>
{
    public async Task<OperationResult<List<CompetitorDto>>> Handle(
        GetFilteredCompetitorsQuery request, 
        CancellationToken cancellationToken)
    {
        var competitors = await genericCompetitorRepository.GetAllAsync(
            cancellationToken, 
            c => c.TournamentCompetitors,
            c => c.GroupCompetitors
        );

        var enumerable = competitors as Competitor[] ?? competitors.ToArray();
        if (!enumerable.Any())
        {
            return OperationResult<List<CompetitorDto>>.Failure("No competitors found.");
        }

        var filteredQuery = enumerable.AsEnumerable();

        if (request.GroupId.HasValue)
        {
            filteredQuery = filteredQuery.Where(c => 
                c.GroupCompetitors.Any(gc => gc.GroupId == request.GroupId.Value)            );
        }
        else
        {
            filteredQuery = filteredQuery.Where(c =>
                c.TournamentCompetitors.Any(tc => tc.TournamentId == request.TournamentId)            );
        }

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var search = request.SearchTerm.Trim().ToLower();
            filteredQuery = filteredQuery.Where(c => c.Name.Contains(search, StringComparison.CurrentCultureIgnoreCase));
        }

        var resultDtoList = filteredQuery
            .OrderBy(c => c.Name)
            .Select(c => new CompetitorDto
            {
                CompetitorId = c.CompetitorId,
                Name = c.Name,
                IsIndividual = c.IsIndividual
            })
            .ToList();

        return OperationResult<List<CompetitorDto>>.Success(resultDtoList);
    }
}