using MediaLakeCore.Domain.CommentReactions;
using System.Threading.Tasks;

namespace MediaLakeCore.Infrastructure.EntityFramework.Repositories
{
    public class CommentReactionRepository : ICommentReactionRepository
    {
        private readonly MediaLakeCoreDbContext _dbContext;

        public CommentReactionRepository(MediaLakeCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(CommentReaction reaction)
        {
            await _dbContext.CommentReactions.AddAsync(reaction);
            await _dbContext.SaveChangesAsync();
        }

        public void Add(CommentReaction reaction)
        {
            _dbContext.CommentReactions.Add(reaction);
            _dbContext.SaveChanges();
        }

        public void Delete(CommentReaction reaction)
        {
            _dbContext.CommentReactions.Remove(reaction);
            _dbContext.SaveChanges();
        }
    }
}
