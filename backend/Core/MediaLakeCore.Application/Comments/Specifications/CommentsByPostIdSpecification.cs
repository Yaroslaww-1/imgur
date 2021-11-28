using Ardalis.Specification;
using MediaLakeCore.Domain.Comments;
using MediaLakeCore.Domain.Posts;

namespace MediaLakeCore.Application.Comments.Specifications
{
    public class CommentsByPostIdSpecification : Specification<Comment>
    {
        public CommentsByPostIdSpecification(PostId postId)
        {
            base.Query
                .Where(c => c.PostId == postId);
        }
    }
}
