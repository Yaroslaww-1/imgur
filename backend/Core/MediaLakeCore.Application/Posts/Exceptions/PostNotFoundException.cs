using MediaLakeCore.BuildingBlocks.Application.Exceptions;
using MediaLakeCore.Domain.Posts;

namespace MediaLakeCore.Application.Posts.Exceptions
{
    public class PostNotFoundException : NotFoundException
    {
        public PostNotFoundException(PostId postId) : base(postId.Value)
        { }
    }
}
