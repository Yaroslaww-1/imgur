using MediaLakeCore.Domain.Posts;
using MediaLakeCore.Domain.Users;
using System;

namespace MediaLakeCore.Domain.Comments
{
    public class Comment
    {
        public CommentId Id { get; private set; }
        public PostId PostId { get; private set; }
        public User CreatedBy { get; private set; }
        public string Content { get; private set; }

        private Comment()
        {
            // Only for EF
        }

        private Comment(User createdBy, PostId postId, string content)
        {
            Id = new CommentId(Guid.NewGuid());
            CreatedBy = createdBy;
            PostId = postId;
            Content = content;
        }

        public static Comment CreateNew(User user, PostId postId, string content)
        {
            return new Comment(user, postId, content);
        }
    }
}
