using MediaLakeCore.BuildingBlocks.Domain;
using MediaLakeCore.Domain.Comments;
using MediaLakeCore.Domain.Users;

namespace MediaLakeCore.Domain.CommentReactions
{
    public interface ICommentReactionToggler : IDomainService
    {
        public void ToggleLike(CommentReaction existingReaction, CommentId commentId, UserId creatorId);
        public void ToggleDislike(CommentReaction existingReaction, CommentId commentId, UserId creatorId);
    }
}
