using Ardalis.Specification;
using MediaLakeCore.Domain.CommentReactions;
using MediaLakeCore.Domain.Comments;
using MediaLakeCore.Domain.Users;
using System.Linq;

namespace MediaLakeCore.Application.CommentReactions.Specifications
{
    public class CommentReactionByCommentIdAndCreatedBySpecification : Specification<CommentReaction>
    {
        public CommentReactionByCommentIdAndCreatedBySpecification(CommentId commentId, UserId createdBy)
        {
            base.Query
                .Where(r => r.CommentId == commentId && r.CreatedBy == createdBy);
        }
    }
}
