using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using MediaLakeCore.Application.Posts.Dtos;
using MediaLakeCore.Application.Users.Specifications;
using MediaLakeCore.BuildingBlocks.Application.ExecutionContext;
using MediaLakeCore.Domain.PostComments;
using MediaLakeCore.Domain.Posts;
using MediaLakeCore.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediaLakeCore.Application.PostComments.CreatePostComment
{
    public class CreatePostCommentCommand : IRequest<Guid>
    {
        public Guid PostId { get; set; }
        public string Content { get; set; }

        public CreatePostCommentCommand(Guid postId, string content)
        {
            PostId = postId;
            Content = content;
        }
    }

    internal class CreatePostCommentCommandHandler : IRequestHandler<CreatePostCommentCommand, Guid>
    {
        private readonly MediaLakeCoreDbContext _dbContext;
        private readonly IPostCommentRepository _postCommentRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public CreatePostCommentCommandHandler(MediaLakeCoreDbContext dbContext, IPostCommentRepository postCommentRepository, IMapper mapper, IUserContext userContext)
        {
            _dbContext = dbContext;
            _postCommentRepository = postCommentRepository;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<Guid> Handle(CreatePostCommentCommand request, CancellationToken cancellationToken)
        {
            var createdBy = await _dbContext.Users
                .WithSpecification(new UserByEmailSpecification(_userContext.Email))
                .FirstAsync();

            var comment = PostComment.CreateNew(createdBy, new PostId(request.PostId), request.Content);

            await _postCommentRepository.AddAsync(comment);

            return comment.Id.Value;
        }
    }
}
