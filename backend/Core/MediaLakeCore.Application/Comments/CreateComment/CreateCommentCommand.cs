using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using MediaLakeCore.Application.Users.Specifications;
using MediaLakeCore.BuildingBlocks.Application.ExecutionContext;
using MediaLakeCore.Domain.Comments;
using MediaLakeCore.Domain.Posts;
using MediaLakeCore.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediaLakeCore.Application.Comments.CreateComment
{
    public class CreateCommentCommand : IRequest<Guid>
    {
        public Guid PostId { get; set; }
        public string Content { get; set; }

        public CreateCommentCommand(Guid postId, string content)
        {
            PostId = postId;
            Content = content;
        }
    }

    internal class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Guid>
    {
        private readonly MediaLakeCoreDbContext _dbContext;
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public CreateCommentCommandHandler(MediaLakeCoreDbContext dbContext, ICommentRepository commentRepository, IMapper mapper, IUserContext userContext)
        {
            _dbContext = dbContext;
            _commentRepository = commentRepository;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<Guid> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var createdBy = await _dbContext.Users
                .WithSpecification(new UserByEmailSpecification(_userContext.Email))
                .FirstAsync();

            var comment = Comment.CreateNew(createdBy, new PostId(request.PostId), request.Content);

            await _commentRepository.AddAsync(comment);

            await _dbContext.SaveChangesAsync();

            return comment.Id.Value;
        }
    }
}
