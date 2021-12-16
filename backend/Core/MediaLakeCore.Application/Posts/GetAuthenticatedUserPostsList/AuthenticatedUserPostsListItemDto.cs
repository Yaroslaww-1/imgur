using System;

namespace MediaLakeCore.Application.Posts.GetAuthenticatedUserPostsList
{
    public class CreatedByDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class AuthenticatedUserReactionDto
    {
        public bool IsLike { get; set; }
    }

    public class AuthenticatedUserPostsListItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int CommentsCount { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public CreatedByDto CreatedBy { get; set; } = null!;
        public AuthenticatedUserReactionDto? AuthenticatedUserReaction { get; set; }
    }
}
