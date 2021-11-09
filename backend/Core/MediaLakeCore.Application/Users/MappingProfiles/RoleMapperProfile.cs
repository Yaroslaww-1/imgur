using AutoMapper;
using MediaLakeCore.Application.Users.Dtos;
using MediaLakeCore.Domain.Users;

namespace MediaLakeCore.Application.Users.MappingProfiles
{
    public class RoleMapperProfile : Profile
    {
        public RoleMapperProfile()
        {
            CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));
        }
    }
}
