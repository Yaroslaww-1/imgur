using MediaLakeCore.Domain.Users;
using System;

namespace MediaLakeCore.Domain.Posts
{
    public class PostComment
    {
        public PostCommentId Id { get; private set; }
        public PostId PostId { get; private set; }
        public User CreatedBy { get; private set; }
        public string Content { get; private set; }

        private PostComment()
        {
            // Only for EF
        }

        private PostComment(User createdBy, PostId postId, string content)
        {
            Id = new PostCommentId(Guid.NewGuid());
            CreatedBy = createdBy;
            PostId = postId;
            Content = content;
        }

        public static PostComment CreateNew(User user, PostId postId, string content)
        {
            return new PostComment(user, postId, content);
        }
    }
}
