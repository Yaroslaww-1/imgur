using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using MediaLakeCore.Application.Posts.Specifications;
using MediaLakeCore.Application.Users.Specifications;
using MediaLakeCore.BuildingBlocks.Application.ExecutionContext;
using MediaLakeCore.Domain.Communities;
using MediaLakeCore.Domain.PostImages;
using MediaLakeCore.Domain.Posts;
using MediaLakeCore.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediaLakeCore.Application.Posts.CreatePost
{
    public class CreatePostCommand : IRequest<Guid>
    {
        public Guid CommunityId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public List<Guid> ImagesIds { get; set; }

        public CreatePostCommand(Guid communityId, string name, string content, List<Guid> imagesIds)
        {
            CommunityId = communityId;
            Name = name;
            Content = content;
            ImagesIds = imagesIds;
        }
    }

    internal class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Guid>
    {
        private readonly MediaLakeCoreDbContext _dbContext;
        private readonly IPostRepository _postRepository;
        private readonly IUserContext _userContext;
        private readonly PostCreatorDomainService _postCreatorDomainService;

        public CreatePostCommandHandler(
            MediaLakeCoreDbContext dbContext,
            IPostRepository postRepository,
            IUserContext userContext,
            PostCreatorDomainService postCreatorDomainService)
        {
            _dbContext = dbContext;
            _postRepository = postRepository;
            _userContext = userContext;
            _postCreatorDomainService = postCreatorDomainService;
        }

        public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var createdBy = await _dbContext.Users
                .WithSpecification(new UserAggregateSpecification())
                .WithSpecification(new UserByEmailSpecification(_userContext.Email))
                .FirstAsync();

            var postDraftImages = await _dbContext.PostImages
                .WithSpecification(new PostImagesByIdsSpecification(request.ImagesIds.Select(i => new PostImageId(i)).ToList()))
                .ToListAsync();

            var post = _postCreatorDomainService.CreateNewPost(
                new CommunityId(request.CommunityId),
                request.Name,
                request.Content,
                createdBy,
                postDraftImages);

            await _postRepository.AddAsync(post);

            await _dbContext.SaveChangesAsync();

            return post.Id.Value;
        }
    }
}
