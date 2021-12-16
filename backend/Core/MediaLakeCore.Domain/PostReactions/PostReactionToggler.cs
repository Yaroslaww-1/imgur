using MediaLakeCore.Domain.Posts;
using MediaLakeCore.Domain.Users;

namespace MediaLakeCore.Domain.PostReactions
{
    public class PostReactionToggler
    {
        private readonly IPostReactionRepository _postReactionRepository;

        public PostReactionToggler(IPostReactionRepository postReactionRepository)
        {
            _postReactionRepository = postReactionRepository;
        }

        public (PostReaction, Post) ToggleLike(PostReaction existingReaction, Post post, UserId likerId)
        {
            if (existingReaction == null)
            {
                var newReaction = PostReaction.CreateNew(post.Id, likerId, true);
                post.AddNewLike();

                _postReactionRepository.Add(newReaction);
                return (newReaction, post);
            }

            if (existingReaction.IsLike == true)
            {
                post.RemoveExistingLike();

                _postReactionRepository.Delete(existingReaction);

                return (null, post);
            }
            else
            {
                existingReaction.Toggle();
                post.AddNewLike();
                post.RemoveExistingDislike();

                return (existingReaction, post);
            }
        }

        public (PostReaction, Post) ToggleDislike(PostReaction existingReaction, Post post, UserId dislikerId)
        {
            if (existingReaction == null)
            {
                var newReaction = PostReaction.CreateNew(post.Id, dislikerId, false);
                post.AddNewDislike();

                _postReactionRepository.Add(newReaction);
                return (newReaction, post);
            }

            if (existingReaction.IsLike == false)
            {
                post.RemoveExistingDislike();

                _postReactionRepository.Delete(existingReaction);

                return (null, post);
            }
            else
            {
                existingReaction.Toggle();
                post.AddNewDislike();
                post.RemoveExistingLike();

                return (existingReaction, post);
            }
        }
    }
}
