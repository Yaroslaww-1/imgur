using System;

namespace MediaLakeCore.Application.Comments.GetCommentsList
{
    public class CreatedByDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class AuthenticatedUserReactionDto
    {
        public bool IsLike { get; set; }
    }

    public class CommentsListItemDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public CreatedByDto CreatedBy { get; set; }
        public AuthenticatedUserReactionDto AuthenticatedUserReaction { get; set; }
    }
}
