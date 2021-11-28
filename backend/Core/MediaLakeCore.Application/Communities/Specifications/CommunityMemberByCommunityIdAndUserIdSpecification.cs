using Ardalis.Specification;
using MediaLakeCore.Domain.Communities;
using MediaLakeCore.Domain.CommunityMember;
using MediaLakeCore.Domain.Users;

namespace MediaLakeCore.Application.Communities.Specifications
{
    public class CommunityMemberByCommunityIdAndUserIdSpecification : Specification<CommunityMember>
    {
        public CommunityMemberByCommunityIdAndUserIdSpecification(CommunityId communityId, UserId userId)
        {
            base.Query
                .Where(cm => cm.CommunityId == communityId && cm.UserId == userId);
        }
    }
}
