using MediaLakeCore.BuildingBlocks.Domain;
using System;

namespace MediaLakeCore.Domain.Communities
{
    public class CommunityId : TypedIdValueBase
    {
        public CommunityId(Guid value)
            : base(value)
        {
        }
    }
}
