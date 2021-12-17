using MediaLakeCore.Domain.PostImages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MediaLakeCore.Infrastructure.EntityFramework.Repositories
{
    public class PostImageRepository : IPostImageRepository
    {
        private readonly MediaLakeCoreDbContext _dbContext;

        public PostImageRepository(MediaLakeCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(PostImage postImage)
        {
            await _dbContext.PostImages.AddAsync(postImage);
        }

        public async Task DeleteAsync(PostImage postImage)
        {
            _dbContext.PostImages.Remove(postImage);
        }

        public async Task<PostImage> GetByIdAsync(PostImageId postImageId)
        {
            return await _dbContext.PostImages.FirstAsync(p => p.Id == postImageId);
        }
    }
}
