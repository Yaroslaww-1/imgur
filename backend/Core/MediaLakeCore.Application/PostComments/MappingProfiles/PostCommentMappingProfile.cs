using AutoMapper;
using MediaLakeCore.Application.PostComments.Dtos;
using MediaLakeCore.Domain.PostComments;

namespace MediaLakeCore.Application.PostComments.MappingProfiles
{
    public class PostCommentMappingProfile : Profile
    {
        public PostCommentMappingProfile()
        {
            CreateMap<PostComment, PostCommentDto>();
        }
    }
}
