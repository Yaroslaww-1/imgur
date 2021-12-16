using MediaLakeCore.Domain.Comments;
using MediaLakeCore.Domain.Users;

namespace MediaLakeCore.Domain.CommentReactions
{
    public class CommentReactionToggler
    {
        private readonly ICommentReactionRepository _commentReactionRepository;

        public CommentReactionToggler(ICommentReactionRepository commentReactionRepository)
        {
            _commentReactionRepository = commentReactionRepository;
        }

        public (CommentReaction, Comment) ToggleLike(CommentReaction existingReaction, Comment comment, UserId likerId)
        {
            if (existingReaction == null)
            {
                var newReaction = CommentReaction.CreateNew(comment.Id, likerId, true);
                comment.AddNewLike();

                _commentReactionRepository.Add(newReaction);
                return (newReaction, comment);
            }

            if (existingReaction.IsLike == true)
            {
                comment.RemoveExistingLike();

                _commentReactionRepository.Delete(existingReaction);

                return (null, comment);
            }
            else
            {
                existingReaction.Toggle();
                comment.AddNewLike();
                comment.RemoveExistingDislike();

                return (existingReaction, comment);
            }
        }

        public (CommentReaction, Comment) ToggleDislike(CommentReaction existingReaction, Comment comment, UserId dislikerId)
        {
            if (existingReaction == null)
            {
                var newReaction = CommentReaction.CreateNew(comment.Id, dislikerId, false);
                comment.AddNewDislike();

                _commentReactionRepository.Add(newReaction);
                return (newReaction, comment);
            }

            if (existingReaction.IsLike == false)
            {
                comment.RemoveExistingDislike();

                _commentReactionRepository.Delete(existingReaction);

                return (null, comment);
            }
            else
            {
                existingReaction.Toggle();
                comment.AddNewDislike();
                comment.RemoveExistingLike();

                return (existingReaction, comment);
            }
        }
    }
}
