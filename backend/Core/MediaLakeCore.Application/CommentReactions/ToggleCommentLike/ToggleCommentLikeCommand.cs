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
        private readonly ICommentReactionToggler _commentReactionToggler;

        public ToggleCommentLikeCommandHandler(
            MediaLakeCoreDbContext dbContext,
            IUserContext userContext,
            ICommentReactionToggler commentReactionToggler)
        {
            _dbContext = dbContext;
            _userContext = userContext;
            _commentReactionToggler = commentReactionToggler;
        }

        public async Task<ToggleCommentLikeDto> Handle(ToggleCommentLikeCommand request, CancellationToken cancellationToken)
        {
            var existingReaction = await _dbContext.CommentReactions
                .WithSpecification(new CommentReactionAggregateSpecification())
                .WithSpecification(new CommentReactionByCommentIdAndCreatedBySpecification(new CommentId(request.CommentId), new UserId(_userContext.UserId)))
                .FirstOrDefaultAsync();

            _commentReactionToggler.ToggleLike(existingReaction, new CommentId(request.CommentId), new UserId(_userContext.UserId));

            await _dbContext.SaveChangesAsync();

            using var connection = _dbContext.Database.GetDbConnection();

            var sql = $@"SELECT * FROM
                        (SELECT COUNT(*) AS {nameof(ToggleCommentLikeDto.LikesCount)} FROM comment_reaction WHERE comment_reaction.comment_id = @CommentId AND is_like = TRUE) AS c1,
                        (SELECT COUNT(*) AS {nameof(ToggleCommentLikeDto.DislikesCount)} FROM comment_reaction WHERE comment_reaction.comment_id = @CommentId AND is_like = FALSE) AS c2";

            var likesDislikesCount = await connection.QueryAsync<ToggleCommentLikeDto>(
                sql,
                new
                {
                    CommentId = request.CommentId
                });

            return likesDislikesCount.First();
        }
    }
}
