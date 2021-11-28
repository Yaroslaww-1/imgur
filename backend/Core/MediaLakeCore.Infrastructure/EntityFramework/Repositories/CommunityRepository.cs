using MediaLakeCore.Domain.Communities;
using System.Threading.Tasks;

namespace MediaLakeCore.Infrastructure.EntityFramework.Repositories
{
    public class CommunityRepository : ICommunityRepository
    {
        private readonly MediaLakeCoreDbContext _dbContext;

        public CommunityRepository(MediaLakeCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Community community)
        {
            await _dbContext.Communities.AddAsync(community);
            await _dbContext.SaveChangesAsync();
        }
    }
}
