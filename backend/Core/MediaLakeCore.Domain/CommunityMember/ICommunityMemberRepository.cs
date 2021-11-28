using System.Threading.Tasks;

namespace MediaLakeCore.Domain.CommunityMember
{
    public interface ICommunityMemberRepository
    {
        public Task AddAsync(CommunityMember communityMember);
        public Task DeleteAsync(CommunityMember communityMember);
    }
}
