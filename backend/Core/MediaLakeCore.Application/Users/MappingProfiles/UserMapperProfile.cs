using AutoMapper;
using MediaLakeCore.Application.Users.Dtos;
using MediaLakeCore.Domain.Users;

namespace MediaLakeCore.Application.Users.MappingProfiles
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));
        }
    }
}
