using AutoMapper;
using MediaLakeCore.Domain.Comments;
using MediaLakeCore.Domain.Users;

namespace MediaLakeCore.Application.Comments.GetCommentsList
{
    public class GetCommentsListMappingProfile : Profile
    {
        public GetCommentsListMappingProfile()
        {
            CreateMap<User, CommentsListItemCreatedByDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));

            CreateMap<Comment, CommentsListItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));
        }
    }
}
