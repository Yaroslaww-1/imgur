using Ardalis.Specification.EntityFrameworkCore;
using MediaLakeCore.Application.CommentReactions.Specifications;
using MediaLakeCore.BuildingBlocks.Application.ExecutionContext;
using MediaLakeCore.Domain.CommentReactions;
using MediaLakeCore.Domain.Comments;
using MediaLakeCore.Domain.Users;
using MediaLakeCore.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediaLakeCore.Application.CommentReactions.ToggleCommentDislike
{
    public class ToggleCommentDislikeCommand : IRequest<ToggleCommentDislikeDto>
    {
        public Guid PostId { get; set; }
        public Guid CommentId { get; set; }

        public ToggleCommentDislikeCommand(Guid postId, Guid commentId)
        {
            PostId = postId;
            CommentId = commentId;
        }
    }

    internal class ToggleCommentDislikeCommandHandler : IRequestHandler<ToggleCommentDislikeCommand, ToggleCommentDislikeDto>
    {
        private readonly MediaLakeCoreDbContext _dbContext;
        private readonly IUserContext _userContext;
        private readonly CommentReactionToggler _commentReactionToggler;
        private readonly ICommentRepository _commentRepository;

        public ToggleCommentDislikeCommandHandler(
            MediaLakeCoreDbContext dbContext,
            IUserContext userContext,
            CommentReactionToggler commentReactionToggler,
            ICommentRepository commentRepository)
        {
            _dbContext = dbContext;
            _userContext = userContext;
            _commentReactionToggler = commentReactionToggler;
            _commentRepository = commentRepository;
        }

        public async Task<ToggleCommentDislikeDto> Handle(ToggleCommentDislikeCommand request, CancellationToken cancellationToken)
        {
            var existingReaction = await _dbContext.CommentReactions
                .WithSpecification(new CommentReactionAggregateSpecification())
                .WithSpecification(new CommentReactionByCommentIdAndCreatedBySpecification(new CommentId(request.CommentId), new UserId(_userContext.UserId)))
                .FirstOrDefaultAsync();

            var comment = await _commentRepository.GetByIdAsync(new CommentId(request.CommentId));

            var (resultingReaction, resultingComment) = _commentReactionToggler.ToggleDislike(existingReaction, comment, new UserId(_userContext.UserId));

            await _dbContext.SaveChangesAsync();

            return new ToggleCommentDislikeDto()
            {
                LikesCount = resultingComment.LikesCount,
                DislikesCount = resultingComment.DislikesCount,
                AuthenticatedUserReaction = resultingReaction != null ? new AuthenticatedUserReactionDto() { IsLike = resultingReaction.IsLike } : null
            };
        }
    }
}
