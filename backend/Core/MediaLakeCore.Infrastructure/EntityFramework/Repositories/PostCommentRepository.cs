using MediaLakeCore.Domain.PostComments;
using System.Threading.Tasks;

namespace MediaLakeCore.Infrastructure.EntityFramework.Repositories.PostComments
{
    public class PostCommentRepository : IPostCommentRepository
    {
        private readonly MediaLakeCoreDbContext _dbContext;

        public PostCommentRepository(MediaLakeCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(PostComment comment)
        {
            await _dbContext.PostComments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
        }
    }
}
