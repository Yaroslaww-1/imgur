using Ardalis.Specification;
using MediaLakeCore.Domain.PostComments;

namespace MediaLakeCore.Application.PostComments.Specifications
{
    public class PostCommentAggregateSpecification : Specification<PostComment>
    {
        public PostCommentAggregateSpecification()
        {
            base.Query
                .Include(pc => pc.CreatedBy);
        }
    }
}
