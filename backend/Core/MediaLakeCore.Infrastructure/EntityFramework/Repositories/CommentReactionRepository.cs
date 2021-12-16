using MediaLakeCore.Domain.CommentReactions;
using MediaLakeCore.Infrastructure.EventBus.Domain;
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
        }

        public void Add(CommentReaction reaction)
        {
            _dbContext.CommentReactions.Add(reaction);
        }

        public void Delete(CommentReaction reaction)
        {
            _dbContext.CommentReactions.Remove(reaction);
        }
    }
}
