using MediaLakeCore.Domain.PostReactions;
using System.Threading.Tasks;

namespace MediaLakeCore.Infrastructure.EntityFramework.Repositories
{
    public class PostReactionRepository : IPostReactionRepository
    {
        private readonly MediaLakeCoreDbContext _dbContext;

        public PostReactionRepository(MediaLakeCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(PostReaction reaction)
        {
            await _dbContext.PostReactions.AddAsync(reaction);
        }

        public void Add(PostReaction reaction)
        {
            _dbContext.PostReactions.Add(reaction);
        }

        public void Delete(PostReaction reaction)
        {
            _dbContext.PostReactions.Remove(reaction);
        }
    }
}
