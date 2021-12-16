namespace MediaLakeCore.Application.PostReactions.TogglePostLike
{
    public class AuthenticatedUserReactionDto
    {
        public bool IsLike { get; set; }
    }

    public class TogglePostLikeDto
    {
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public AuthenticatedUserReactionDto? AuthenticatedUserReaction { get; set; }
    }
}
