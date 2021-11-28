using MediaLakeCore.Domain.Comments;
using MediaLakeCore.Domain.Users;
using System;

namespace MediaLakeCore.Domain.CommentReactions
{
    public class CommentReaction
    {
        public CommentReactionId Id { get; private set; }
        public CommentId CommentId { get; private set; }
        public UserId CreatedBy { get; private set; }
        public bool IsLike { get; private set; }

        private CommentReaction()
        {
            // Only for EF
        }

        private CommentReaction(CommentId commentId, UserId createdBy, bool isLike)
        {
            Id = new CommentReactionId(Guid.NewGuid());
            CommentId = commentId;
            CreatedBy = createdBy;
            IsLike = isLike;
        }

        public static CommentReaction CreateNew(CommentId commentId, UserId createdBy, bool isLike)
        {
            return new CommentReaction(commentId, createdBy, isLike);
        }

        public void Toggle()
        {
            IsLike = !IsLike;
        }
    }
}
