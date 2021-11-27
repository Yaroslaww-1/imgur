using MediaLakeCore.Domain.PostReactions;
using MediaLakeCore.Domain.Posts;
using MediaLakeCore.Domain.Users;

namespace MediaLakeCore.Application.PostReactions
{
    #nullable enable
    public class PostReactionsToggler : IPostReactionsToggler
    {
        private readonly IPostReactionRepository _postReactionRepository;

        public PostReactionsToggler(IPostReactionRepository postReactionRepository)
        {
            _postReactionRepository = postReactionRepository;
        }


        public void ToggleLike(PostReaction? existingPostReaction, PostId postId, UserId creatorId)
        {
            if (existingPostReaction == null)
            {
                var newPostReaction = PostReaction.CreateNew(postId, creatorId, true);
                _postReactionRepository.Add(newPostReaction);
                return;
            }

            var isPostAlreadyLiked = existingPostReaction.IsLike == true;
            if (isPostAlreadyLiked)
            {
                _postReactionRepository.Delete(existingPostReaction);
            }
            else
            {
                existingPostReaction.Toggle();
            }
        }

        public void ToggleDislike(PostReaction? existingPostReaction, PostId postId, UserId creatorId)
        {
            if (existingPostReaction == null)
            {
                var newPostReaction = PostReaction.CreateNew(postId, creatorId, false);
                _postReactionRepository.Add(newPostReaction);
                return;
            }

            var isPostAlreadyDisliked = existingPostReaction.IsLike == false;
            if (isPostAlreadyDisliked)
            {
                _postReactionRepository.Delete(existingPostReaction);
            }
            else
            {
                existingPostReaction.Toggle();
            }
        }
    }
    #nullable disable
}
