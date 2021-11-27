using MediaLakeCore.BuildingBlocks.Domain;
using System;

namespace MediaLakeCore.Domain.PostReactions
{
    public class PostReactionId : TypedIdValueBase
    {
        public PostReactionId(Guid value)
            : base(value)
        {
        }
    }
}
