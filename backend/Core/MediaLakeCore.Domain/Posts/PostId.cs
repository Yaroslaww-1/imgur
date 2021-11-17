using MediaLakeCore.BuildingBlocks.Domain;
using System;

namespace MediaLakeCore.Domain.Posts
{
    public class PostId : TypedIdValueBase
    {
        public PostId(Guid value)
            : base(value)
        {
        }
    }
}
