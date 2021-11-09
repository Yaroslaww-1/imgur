using MediaLakeCore.BuildingBlocks.Domain;
using System;

namespace MediaLakeCore.Domain.Users
{
    public class UserId : TypedIdValueBase
    {
        public UserId(Guid value)
            : base(value)
        {
        }
    }
}
