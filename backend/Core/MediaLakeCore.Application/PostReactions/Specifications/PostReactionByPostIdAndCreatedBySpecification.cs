using Ardalis.Specification;
using MediaLakeCore.Domain.PostReactions;
using MediaLakeCore.Domain.Posts;
using MediaLakeCore.Domain.Users;

namespace MediaLakeCore.Application.PostReactions.Specifications
{
    public class PostReactionByPostIdAndCreatedBySpecification : Specification<PostReaction>
    {
        public PostReactionByPostIdAndCreatedBySpecification(PostId postId, UserId createdBy)
        {
            base.Query
                .Where(c => c.PostId == postId && c.CreatedBy == createdBy);
        }
    }
}
