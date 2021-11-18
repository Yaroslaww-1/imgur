using MediaLakeCore.BuildingBlocks.Domain;
using System;

namespace MediaLakeCore.Domain.Users
{
    public class RoleId : TypedIdValueBase
    {
        public RoleId(Guid value)
            : base(value)
        {
        }
    }
}
