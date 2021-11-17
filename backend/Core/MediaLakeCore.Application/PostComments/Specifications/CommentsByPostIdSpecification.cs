using Ardalis.Specification;
using MediaLakeCore.Domain.PostComments;
using MediaLakeCore.Domain.Posts;

namespace MediaLakeCore.Application.PostComments.Specifications
{
    public class CommentsByPostIdSpecification : Specification<PostComment>
    {
        public CommentsByPostIdSpecification(PostId postId)
        {
            base.Query
                .Where(c => c.PostId == postId);
        }
    }
}
