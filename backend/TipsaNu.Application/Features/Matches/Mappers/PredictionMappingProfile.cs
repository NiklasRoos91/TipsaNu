using AutoMapper;
using TipsaNu.Application.Features.Matches.DTOs;
using TipsaNu.Domain.Entities;

namespace TipsaNu.Application.Features.Matches.Mappers
{
    public class PredictionMappingProfile : Profile
    {
        public PredictionMappingProfile()
        {
            CreateMap<Prediction, PredictionDto>();
        }
    }
}
