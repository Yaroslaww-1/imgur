using Ardalis.Specification;
using MediaLakeCore.Domain.Posts;

namespace MediaLakeCore.Application.Posts.Specifications
{
    public class PostByIdSpecification : Specification<Post>
    {
        public PostByIdSpecification(PostId postId)
        {
            base.Query.Where(p => p.Id == postId);
        }
    }
}
