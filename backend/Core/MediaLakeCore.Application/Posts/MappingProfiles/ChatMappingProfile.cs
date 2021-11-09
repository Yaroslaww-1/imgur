using AutoMapper;
using MediaLakeCore.Application.Posts.Dtos;
using MediaLakeCore.Domain.Posts;

namespace MediaLakeCore.Application.Posts.MappingProfiles
{
    public class PostMappingProfile : Profile
    {
        public PostMappingProfile()
        {
            CreateMap<PostComment, PostCommentDto>();
            CreateMap<Post, PostDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));
        }
    }
}
