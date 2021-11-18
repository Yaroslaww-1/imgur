using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using MediaLakeCore.Application.Posts.Dtos;
using MediaLakeCore.Application.Users.Specifications;
using MediaLakeCore.BuildingBlocks.Application.ExecutionContext;
using MediaLakeCore.Domain.Posts;
using MediaLakeCore.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediaLakeCore.Application.Posts.CreatePost
{
    public class CreatePostCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Content { get; set; }

        public CreatePostCommand(string name, string content)
        {
            Name = name;
            Content = content;
        }
    }

    internal class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Guid>
    {
        private readonly MediaLakeCoreDbContext _dbContext;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public CreatePostCommandHandler(MediaLakeCoreDbContext dbContext, IPostRepository postRepository, IMapper mapper, IUserContext userContext)
        {
            _dbContext = dbContext;
            _postRepository = postRepository;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var createdBy = await _dbContext.Users
                .WithSpecification(new UserAggregateSpecification())
                .WithSpecification(new UserByEmailSpecification(_userContext.Email))
                .FirstAsync();

            var post = Post.CreateNew(request.Name, request.Content, createdBy);

            await _postRepository.AddAsync(post);

            return post.Id.Value;
        }
    }
}
