namespace MediaLakeCore.Application.PostReactions.TogglePostDislike
{
    public class AuthenticatedUserReactionDto
    {
        public bool IsLike { get; set; }
    }

    public class TogglePostDislikeDto
    {
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public AuthenticatedUserReactionDto? AuthenticatedUserReaction { get; set; }
    }
}
