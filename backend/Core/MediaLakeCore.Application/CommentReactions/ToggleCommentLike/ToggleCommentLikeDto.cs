namespace MediaLakeCore.Application.CommentReactions.ToggleCommentLike
{
    public class AuthenticatedUserReactionDto
    {
        public bool IsLike { get; set; }
    }

    public class ToggleCommentLikeDto
    {
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public AuthenticatedUserReactionDto? AuthenticatedUserReaction { get; set; }
    }
}
