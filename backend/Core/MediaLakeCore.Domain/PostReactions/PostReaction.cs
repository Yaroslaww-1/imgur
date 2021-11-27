using MediaLakeCore.Domain.Posts;
using MediaLakeCore.Domain.Users;
using System;

namespace MediaLakeCore.Domain.PostReactions
{
    public class PostReaction
    {
        public PostReactionId Id { get; private set; }
        public PostId PostId { get; private set; }
        public UserId CreatedBy { get; private set; }
        public bool IsLike { get; private set; }

        private PostReaction()
        {
            // Only for EF
        }

        private PostReaction(PostId postId, UserId createdBy, bool isLike)
        {
            Id = new PostReactionId(Guid.NewGuid());
            PostId = postId;
            CreatedBy = createdBy;
            IsLike = isLike;
        }

        public static PostReaction CreateNew(PostId postId, UserId createdBy, bool isLike)
        {
            return new PostReaction(postId, createdBy, isLike);
        }

        public void Toggle()
        {
            IsLike = !IsLike;
        }
    }
}
