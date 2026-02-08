using AutoMapper;
using TipsaNu.Application.Features.Predictions.DTOs;
using TipsaNu.Domain.Entities;

namespace TipsaNu.Application.Features.Predictions.Mappers
{
    public class PredictionMappingProfile : Profile
    {
        public PredictionMappingProfile()
        {
            CreateMap<Prediction, MatchPredictionDto>()
                .ForMember(dest => dest.MatchId, opt => opt.MapFrom(src => src.MatchId))
                .ForMember(dest => dest.PredictedHomeScore, opt => opt.MapFrom(src => src.PredictedHomeScore))
                .ForMember(dest => dest.PredictedAwayScore, opt => opt.MapFrom(src => src.PredictedAwayScore))
                .ForMember(dest => dest.PredictedWinnerId, opt => opt.MapFrom(src => src.PredictedWinnerId))
                .ForMember(dest => dest.PointsAwarded, opt => opt.MapFrom(src => src.PointsAwarded))
                .ForMember(dest => dest.SubmittedAt, opt => opt.MapFrom(src => src.SubmittedAt))
                // Match info
                .ForMember(dest => dest.HomeTeamName, opt => opt.MapFrom(src => src.Match.HomeCompetitor.Name))
                .ForMember(dest => dest.AwayTeamName, opt => opt.MapFrom(src => src.Match.AwayCompetitor.Name))
                .ForMember(dest => dest.MatchStartTime, opt => opt.MapFrom(src => src.Match.StartTime));
        }
    }
}
