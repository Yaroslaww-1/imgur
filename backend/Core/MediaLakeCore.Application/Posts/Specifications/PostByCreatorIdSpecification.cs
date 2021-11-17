using Ardalis.Specification;
using MediaLakeCore.Domain.Posts;
using MediaLakeCore.Domain.Users;

namespace MediaLakeCore.Application.Posts.Specifications
{
    public class PostByCreatorIdSpecification : Specification<Post>
    {
        public PostByCreatorIdSpecification(UserId userId)
        {
            base.Query.Where(u => u.CreatedBy.Id == userId);
        }
    }
}
