using MediaLakeCore.BuildingBlocks.Application;
using MediaLakeCore.BuildingBlocks.Application.ExecutionContext;
using MediaLakeCore.Domain.PostImages;
using MediaLakeCore.Domain.Posts;
using MediaLakeCore.Infrastructure.EntityFramework;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediaLakeCore.Application.Posts.UploadPostImage
{
    public class UploadPostImageCommand : IRequest<PostImageDto>
    {
        public string ImageContent { get; set; }

        public UploadPostImageCommand(string imageContent)
        {
            ImageContent = imageContent;
        }
    }

    internal class UploadPostImageCommandHandler : IRequestHandler<UploadPostImageCommand, PostImageDto>
    {
        private readonly MediaLakeCoreDbContext _dbContext;
        private readonly IUserContext _userContext;
        private readonly IFileService _fileService;
        private readonly IPostImageRepository _postImageRepository;

        public UploadPostImageCommandHandler(
            MediaLakeCoreDbContext dbContext,
            IUserContext userContext,
            IFileService fileService,
            IPostImageRepository postImageRepository)
        {
            _dbContext = dbContext;
            _userContext = userContext;
            _fileService = fileService;
            _postImageRepository = postImageRepository;
        }

        public async Task<PostImageDto> Handle(UploadPostImageCommand request, CancellationToken cancellationToken)
        {
            var imageUrl = await _fileService.UploadPublicFileAsync(request.ImageContent);

            var postImage = PostImage.CreateNewDraft(imageUrl);

            await _postImageRepository.AddAsync(postImage);

            await _dbContext.SaveChangesAsync();

            return new PostImageDto()
            {
                Id = postImage.Id.Value,
                Url = postImage.Url
            };
        }
    }
}
