using MediaLakeCore.Domain.CommentReactions;
using MediaLakeCore.Domain.Comments;
using MediaLakeCore.Domain.Users;

namespace MediaLakeCore.Application.CommentReactions
{
    #nullable enable
    public class CommentReactionToggler : ICommentReactionToggler
    {
        private readonly ICommentReactionRepository _commentReactionRepository;

        public CommentReactionToggler(ICommentReactionRepository commentReactionRepository)
        {
            _commentReactionRepository = commentReactionRepository;
        }


        public void ToggleLike(CommentReaction? existingPostReaction, CommentId commentId, UserId creatorId)
        {
            if (existingPostReaction == null)
            {
                var newPostReaction = CommentReaction.CreateNew(commentId, creatorId, true);
                _commentReactionRepository.Add(newPostReaction);
                return;
            }

            var isPostAlreadyLiked = existingPostReaction.IsLike == true;
            if (isPostAlreadyLiked)
            {
                _commentReactionRepository.Delete(existingPostReaction);
            }
            else
            {
                existingPostReaction.Toggle();
            }
        }

        public void ToggleDislike(CommentReaction? existingPostReaction, CommentId commentId, UserId creatorId)
        {
            if (existingPostReaction == null)
            {
                var newPostReaction = CommentReaction.CreateNew(commentId, creatorId, false);
                _commentReactionRepository.Add(newPostReaction);
                return;
            }

            var isPostAlreadyDisliked = existingPostReaction.IsLike == false;
            if (isPostAlreadyDisliked)
            {
                _commentReactionRepository.Delete(existingPostReaction);
            }
            else
            {
                existingPostReaction.Toggle();
            }
        }
    }
    #nullable disable
}
