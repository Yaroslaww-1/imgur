using Ardalis.Specification.EntityFrameworkCore;
using Dapper;
using MediaLakeCore.Application.CommentReactions.Specifications;
using MediaLakeCore.BuildingBlocks.Application.ExecutionContext;
using MediaLakeCore.Domain.CommentReactions;
using MediaLakeCore.Domain.Comments;
using MediaLakeCore.Domain.Users;
using MediaLakeCore.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediaLakeCore.Application.CommentReactions.ToggleCommentLike
{
    public class ToggleCommentLikeCommand : IRequest<ToggleCommentLikeDto>
    {
        public Guid PostId { get; set; }
        public Guid CommentId { get; set; }

        public ToggleCommentLikeCommand(Guid postId, Guid commentId)
        {
            PostId = postId;
            CommentId = commentId;
        }
    }

    internal class ToggleCommentLikeCommandHandler : IRequestHandler<ToggleCommentLikeCommand, ToggleCommentLikeDto>
    {
        private readonly MediaLakeCoreDbContext _dbContext;
        private readonly IUserContext _userContext;
        private readonly CommentReactionToggler _commentReactionToggler;
        private readonly ICommentRepository _commentRepository;

        public ToggleCommentLikeCommandHandler(
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

        public async Task<ToggleCommentLikeDto> Handle(ToggleCommentLikeCommand request, CancellationToken cancellationToken)
        {
            var existingReaction = await _dbContext.CommentReactions
                .WithSpecification(new CommentReactionAggregateSpecification())
                .WithSpecification(new CommentReactionByCommentIdAndCreatedBySpecification(new CommentId(request.CommentId), new UserId(_userContext.UserId)))
                .FirstOrDefaultAsync();

            var targetComment = await _commentRepository.GetByIdAsync(new CommentId(request.CommentId));

            var (resultingReaction, resultingComment) = _commentReactionToggler.ToggleLike(existingReaction, targetComment, new UserId(_userContext.UserId));

            await _dbContext.SaveChangesAsync();

            return new ToggleCommentLikeDto()
            {
                LikesCount = resultingComment.LikesCount,
                DislikesCount = resultingComment.DislikesCount,
                AuthenticatedUserReaction = resultingReaction != null ? new AuthenticatedUserReactionDto() { IsLike = resultingReaction.IsLike } : null
            };
        }
    }
}
