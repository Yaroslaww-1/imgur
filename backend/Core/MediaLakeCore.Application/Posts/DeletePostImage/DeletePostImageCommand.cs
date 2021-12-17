using MediaLakeCore.BuildingBlocks.Application;
using MediaLakeCore.Domain.PostImages;
using MediaLakeCore.Infrastructure.EntityFramework;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediaLakeCore.Application.Posts.DeletePostImage
{
    public class DeletePostImageCommand : IRequest<Unit>
    {
        public Guid ImageId { get; set; }

        public DeletePostImageCommand(Guid imageId)
        {
            ImageId = imageId;
        }
    }

    internal class DeletePostImageCommandHandler : IRequestHandler<DeletePostImageCommand, Unit>
    {
        private readonly MediaLakeCoreDbContext _dbContext;
        private readonly IFileService _fileService;
        private readonly IPostImageRepository _postImageRepository;

        public DeletePostImageCommandHandler(
            MediaLakeCoreDbContext dbContext,
            IFileService fileService,
            IPostImageRepository postImageRepository)
        {
            _dbContext = dbContext;
            _fileService = fileService;
            _postImageRepository = postImageRepository;
        }

        public async Task<Unit> Handle(DeletePostImageCommand request, CancellationToken cancellationToken)
        {
            var image = await _postImageRepository.GetByIdAsync(new PostImageId(request.ImageId));

            await _fileService.DeletePublicFileByUrlAsync(image.Url);

            await _postImageRepository.DeleteAsync(image);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
