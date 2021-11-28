using MediaLakeCore.Domain.CommunityMember;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLakeCore.Infrastructure.EntityFramework.Repositories
{
    public class CommunityMemberRepository : ICommunityMemberRepository
    {
        private readonly MediaLakeCoreDbContext _dbContext;

        public CommunityMemberRepository(MediaLakeCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(CommunityMember communityMember)
        {
            await _dbContext.CommunityMembers.AddAsync(communityMember);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(CommunityMember communityMember)
        {
            _dbContext.CommunityMembers.Remove(communityMember);
            await _dbContext.SaveChangesAsync();
        }
    }
}
