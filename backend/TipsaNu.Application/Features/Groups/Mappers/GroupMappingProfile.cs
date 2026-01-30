using AutoMapper;
using TipsaNu.Application.Features.Groups.DTOs;
using TipsaNu.Domain.Entities;

namespace TipsaNu.Application.Features.Groups.Mappers
{
    public class GroupMappingProfile : Profile
    {
        public GroupMappingProfile()
        {
            CreateMap<Group, GroupDto>();
        }
    }
}
