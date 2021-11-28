using System.Threading.Tasks;

namespace MediaLakeCore.Domain.Communities
{
    public interface ICommunityRepository
    {
        public Task AddAsync(Community community);
        public Task<Community> GetByIdAsync(CommunityId communityId);
    }
}
