using MediaLakeCore.Domain.Comments;
using MediaLakeCore.Infrastructure.EventBus.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MediaLakeCore.Infrastructure.EntityFramework.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly MediaLakeCoreDbContext _dbContext;

        public CommentRepository(MediaLakeCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Comment comment)
        {
            await _dbContext.Comments.AddAsync(comment);
        }

        public async Task<Comment> GetByIdAsync(CommentId commentId)
        {
            return await _dbContext.Comments.FirstAsync(c => c.Id == commentId);
        }
    }
}
