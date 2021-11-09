using Ardalis.Specification;
using MediaLakeCore.Domain.Posts;

namespace MediaLakeCore.Application.Chats.Specifications
{
    public class PostAggregateSpecification : Specification<Post>
    {
        public PostAggregateSpecification()
        {
            base.Query
                .Include(c => c.Comments)
                .ThenInclude(m => m.CreatedBy);
        }
    }
}
