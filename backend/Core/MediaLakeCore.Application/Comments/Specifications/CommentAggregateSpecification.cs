using Ardalis.Specification;
using MediaLakeCore.Domain.Comments;

namespace MediaLakeCore.Application.Comments.Specifications
{
    public class CommentAggregateSpecification : Specification<Comment>
    {
        public CommentAggregateSpecification()
        {
            base.Query
                .Include(pc => pc.CreatedBy);
        }
    }
}
