using Ardalis.Specification;
using MediaLakeCore.Domain.Posts;

namespace MediaLakeCore.Application.Posts.Specifications
{
    public class PostAggregateSpecification : Specification<Post>
    {
        public PostAggregateSpecification()
        {
            base.Query
                .Include(p => p.CreatedBy);
        }
    }
}
