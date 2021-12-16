using MediaLakeCore.Domain.Posts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MediaLakeCore.Infrastructure.EntityFramework.Repositories.Posts
{
    public class PostRepository : IPostRepository
    {
        private readonly MediaLakeCoreDbContext _dbContext;

        public PostRepository(MediaLakeCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Post post)
        {
            await _dbContext.Posts.AddAsync(post);
        }

        public async Task<Post> GetByIdAsync(PostId postId)
        {
            return await _dbContext.Posts.FirstAsync(c => c.Id == postId);
        }
    }
}
