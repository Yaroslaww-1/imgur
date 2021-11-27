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

namespace MediaLakeCore.Application.PostReactions.TogglePostDislike
{
    public class TogglePostDislikeCommand : IRequest<TogglePostDislikeDto>
    {
        public Guid PostId { get; set; }

        public TogglePostDislikeCommand(Guid postId)
        {
            PostId = postId;
        }
    }

    internal class TogglePostDislikeCommandHandler : IRequestHandler<TogglePostDislikeCommand, TogglePostDislikeDto>
    {
        private readonly MediaLakeCoreDbContext _dbContext;
        private readonly IUserContext _userContext;
        private readonly IPostReactionsToggler _postReactionsToggler;

        public TogglePostDislikeCommandHandler(
            MediaLakeCoreDbContext dbContext,
            IUserContext userContext,
            IPostReactionsToggler postReactionsToggler)
        {
            _dbContext = dbContext;
            _userContext = userContext;
            _postReactionsToggler = postReactionsToggler;
        }

        public async Task<TogglePostDislikeDto> Handle(TogglePostDislikeCommand request, CancellationToken cancellationToken)
        {
            var existingPostComment = await _dbContext.PostReactions
                .WithSpecification(new PostReactionAggregateSpecification())
                .WithSpecification(new PostReactionByPostIdAndCreatedBySpecification(new PostId(request.PostId), new UserId(_userContext.UserId)))
                .FirstOrDefaultAsync();

            _postReactionsToggler.ToggleDislike(existingPostComment, new PostId(request.PostId), new UserId(_userContext.UserId));

            await _dbContext.SaveChangesAsync();

            using var connection = _dbContext.Database.GetDbConnection();

            var sql = $@"SELECT * FROM
                        (SELECT COUNT(*) AS {nameof(TogglePostDislikeDto.LikesCount)} FROM post_reaction post_reaction WHERE post_reaction.post_id = @PostId AND is_like = TRUE) AS c1,
                        (SELECT COUNT(*) AS {nameof(TogglePostDislikeDto.DislikesCount)} FROM post_reaction post_reaction WHERE post_reaction.post_id = @PostId AND is_like = FALSE) AS c2";

            var likesDislikesCount = await connection.QueryAsync<TogglePostDislikeDto>(
                sql,
                new
                {
                    PostId = request.PostId
                });

            return likesDislikesCount.First();
        }
    }
}
