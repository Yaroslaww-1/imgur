namespace MediaLakeCore.Application.CommentReactions.ToggleCommentDislike
{
    public class AuthenticatedUserReactionDto
    {
        public bool IsLike { get; set; }
    }

    public class ToggleCommentDislikeDto
    {
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public AuthenticatedUserReactionDto? AuthenticatedUserReaction { get; set; }
    }
}
