using TipsaNu.Application.AdminFeatures.AdminExtraBets.DTOs;
using TipsaNu.Application.Features.ExtraBets.DTOs;
using TipsaNu.Domain.Entities;

namespace TipsaNu.Application.Features.ExtraBets.Mappers
{
    public static class ExtraBetMappingExtensions
    {
        public static ExtraBetOption ToEntity(this CreateExtraBetOptionDto dto)
        {
            return new ExtraBetOption
            {
                TournamentId = dto.TournamentId,
                MatchId = dto.MatchId,
                Name = dto.Name,
                Description = dto.Description,
                Points = dto.Points,
                ExpiresAt = dto.ExpiresAt,
                AllowCustomChoice = dto.AllowCustomChoice,
                
                ExtraBetOptionChoices = dto.Choices.Select(v => new ExtraBetOptionChoice
                {
                    Value = v
                }).ToList()
            };
        }
        
        public static ExtraBetOptionDto ToDto(this ExtraBetOption entity)
        {
            return new ExtraBetOptionDto
            {
                OptionId = entity.OptionId,
                TournamentId = entity.TournamentId,
                MatchId = entity.MatchId,
                Name = entity.Name,
                Description = entity.Description,
                Points = entity.Points,
                ExpiresAt = entity.ExpiresAt,
                AllowCustomChoice = entity.AllowCustomChoice,
        
                Choices = entity.ExtraBetOptionChoices?
                    .Select(c => c.Value)
                    .ToList() ?? new List<string>()
            };
        }
        
        public static ExtraBetDto ToDto(this ExtraBet entity)
        {
            return new ExtraBetDto
            {
                ExtraBetId = entity.ExtraBetId,
                OptionId = entity.OptionId,
                Value = entity.Value,
                PointsAwarded = entity.PointsAwarded,
                SubmittedAt = entity.SubmittedAt 
            };
        }
        
        public static ExtraBetOptionCorrectValueDto ToDto(this ExtraBetOptionCorrectValue entity)
        {
            return new ExtraBetOptionCorrectValueDto
            {
                CorrectValueId = entity.CorrectValueId,
                OptionId = entity.OptionId,
                Value = entity.Value
            };
        }
    }
}