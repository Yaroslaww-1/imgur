using MediaLakeCore.BuildingBlocks.Domain;
using MediaLakeCore.Domain.Posts;
using MediaLakeCore.Domain.Users;

namespace MediaLakeCore.Domain.PostReactions
{
    public interface IPostReactionToggler : IDomainService
    {
        public void ToggleLike(PostReaction existingPostReaction, PostId postId, UserId creatorId);
        public void ToggleDislike(PostReaction existingPostReaction, PostId postId, UserId creatorId);
    }
}
