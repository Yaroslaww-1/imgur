using AutoMapper;
using MediaLakeCore.Application.Posts.Dtos;
using MediaLakeCore.Domain.Posts;

namespace MediaLakeCore.Application.Posts.MappingProfiles
{
    public class PostMappingProfile : Profile
    {
        public PostMappingProfile()
        {
            CreateMap<Post, PostForListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));

            CreateMap<Post, PostByIdCreatedByDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));
        }
    }
}
