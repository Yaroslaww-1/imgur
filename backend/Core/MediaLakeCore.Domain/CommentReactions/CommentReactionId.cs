using MediaLakeCore.BuildingBlocks.Domain;
using System;

namespace MediaLakeCore.Domain.CommentReactions
{
    public class CommentReactionId : TypedIdValueBase
    {
        public CommentReactionId(Guid value)
            : base(value)
        {
        }
    }
}
