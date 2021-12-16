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
        private readonly PostReactionToggler _postReactionsToggler;
        private readonly IPostRepository _postRepository;

        public TogglePostDislikeCommandHandler(
            MediaLakeCoreDbContext dbContext,
            IUserContext userContext,
            PostReactionToggler postReactionsToggler,
            IPostRepository postRepository)
        {
            _dbContext = dbContext;
            _userContext = userContext;
            _postReactionsToggler = postReactionsToggler;
            _postRepository = postRepository;
        }

        public async Task<TogglePostDislikeDto> Handle(TogglePostDislikeCommand request, CancellationToken cancellationToken)
        {
            var existingReaction = await _dbContext.PostReactions
                .WithSpecification(new PostReactionAggregateSpecification())
                .WithSpecification(new PostReactionByPostIdAndCreatedBySpecification(new PostId(request.PostId), new UserId(_userContext.UserId)))
                .FirstOrDefaultAsync();

            var targetPost = await _postRepository.GetByIdAsync(new PostId(request.PostId));

            var (resultingReaction, resultingPost) = _postReactionsToggler.ToggleDislike(existingReaction, targetPost, new UserId(_userContext.UserId));

            await _dbContext.SaveChangesAsync();

            return new TogglePostDislikeDto()
            {
                LikesCount = resultingPost.LikesCount,
                DislikesCount = resultingPost.DislikesCount,
                AuthenticatedUserReaction = resultingReaction != null ? new AuthenticatedUserReactionDto() { IsLike = resultingReaction.IsLike } : null
            };
        }
    }
}
