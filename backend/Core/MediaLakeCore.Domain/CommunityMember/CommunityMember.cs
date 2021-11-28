using MediaLakeCore.Domain.Communities;
using MediaLakeCore.Domain.Users;
using System;

namespace MediaLakeCore.Domain.CommunityMember
{
    public class CommunityMember
    {
        public CommunityMemberId Id { get; private set; }
        public CommunityId CommunityId { get; private set; }
        public UserId UserId { get; private set; }

        private CommunityMember()
        {
            // Only for EF
        }

        private CommunityMember(CommunityId communityId, UserId userId)
        {
            Id = new CommunityMemberId(Guid.NewGuid());
            CommunityId = communityId;
            UserId = userId;
        }

        public static CommunityMember CreateNew(CommunityId communityId, UserId userId)
        {
            return new CommunityMember(communityId, userId);
        }
    }
}
