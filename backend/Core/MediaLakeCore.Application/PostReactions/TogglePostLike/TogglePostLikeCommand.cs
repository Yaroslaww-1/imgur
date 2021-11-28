using Ardalis.Specification.EntityFrameworkCore;
using Dapper;
using MediaLakeCore.Application.PostReactions.Specifications;
using MediaLakeCore.BuildingBlocks.Application.ExecutionContext;
using MediaLakeCore.Domain.PostReactions;
using MediaLakeCore.Domain.Posts;
using MediaLakeCore.Domain.Users;
using MediaLakeCore.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediaLakeCore.Application.PostReactions.TogglePostLike
{
    public class TogglePostLikeCommand : IRequest<TogglePostLikeDto>
    {
        public Guid PostId { get; set; }

        public TogglePostLikeCommand(Guid postId)
        {
            PostId = postId;
        }
    }

    internal class TogglePostLikeCommandHandler : IRequestHandler<TogglePostLikeCommand, TogglePostLikeDto>
    {
        private readonly MediaLakeCoreDbContext _dbContext;
        private readonly IUserContext _userContext;
        private readonly IPostReactionToggler _postReactionsToggler;

        public TogglePostLikeCommandHandler(
            MediaLakeCoreDbContext dbContext,
            IUserContext userContext,
            IPostReactionToggler postReactionsToggler)
        {
            _dbContext = dbContext;
            _userContext = userContext;
            _postReactionsToggler = postReactionsToggler;
        }

        public async Task<TogglePostLikeDto> Handle(TogglePostLikeCommand request, CancellationToken cancellationToken)
        {
            var existingPostComment = await _dbContext.PostReactions
                .WithSpecification(new PostReactionAggregateSpecification())
                .WithSpecification(new PostReactionByPostIdAndCreatedBySpecification(new PostId(request.PostId), new UserId(_userContext.UserId)))
                .FirstOrDefaultAsync();

            _postReactionsToggler.ToggleLike(existingPostComment, new PostId(request.PostId), new UserId(_userContext.UserId));

            await _dbContext.SaveChangesAsync(cancellationToken);

            using var connection = _dbContext.Database.GetDbConnection();

            var sql = $@"SELECT * FROM
                        (SELECT COUNT(*) AS {nameof(TogglePostLikeDto.LikesCount)} FROM post_reaction post_reaction WHERE post_reaction.post_id = @PostId AND is_like = TRUE) AS c1,
                        (SELECT COUNT(*) AS {nameof(TogglePostLikeDto.DislikesCount)} FROM post_reaction post_reaction WHERE post_reaction.post_id = @PostId AND is_like = FALSE) AS c2";

            var likesDislikesCount = await connection.QueryAsync<TogglePostLikeDto>(
                sql,
                new
                {
                    PostId = request.PostId
                });

            return likesDislikesCount.First();
        }
    }
}
