using System.Threading.Tasks;

namespace MediaLakeCore.Domain.Posts
{
    public interface IPostRepository
    {
        public Task<Post> GetByIdAsync(PostId postId);
        public Task AddAsync(Post post);
    }
}
