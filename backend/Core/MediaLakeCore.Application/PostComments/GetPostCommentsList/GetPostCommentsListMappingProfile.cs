using AutoMapper;
using MediaLakeCore.Domain.PostComments;
using MediaLakeCore.Domain.Users;

namespace MediaLakeCore.Application.PostComments.GetPostCommentsList
{
    public class GetPostCommentsListMappingProfile : Profile
    {
        public GetPostCommentsListMappingProfile()
        {
            CreateMap<User, PostCommentsListItemCreatedByDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));

            CreateMap<PostComment, PostCommentsListItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));
        }
    }
}
