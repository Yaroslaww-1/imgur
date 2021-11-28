using MediaLakeCore.BuildingBlocks.Domain;
using System;

namespace MediaLakeCore.Domain.CommunityMember
{
    public class CommunityMemberId : TypedIdValueBase
    {
        public CommunityMemberId(Guid value)
            : base(value)
        {
        }
    }
}
