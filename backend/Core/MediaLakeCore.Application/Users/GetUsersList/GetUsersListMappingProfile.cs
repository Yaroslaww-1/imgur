using AutoMapper;
using MediaLakeCore.Domain.Users;

namespace MediaLakeCore.Application.Users.GetUsersList
{
    public class GetUsersListMappingProfile : Profile
    {
        public GetUsersListMappingProfile()
        {
            CreateMap<Role, UsersListItemRoleDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));

            CreateMap<User, UsersListItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));
        }
    }
}
