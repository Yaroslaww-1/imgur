using MediaLakeCore.BuildingBlocks.Domain;
using System;

namespace MediaLakeCore.Domain.Comments
{
    public class CommentId : TypedIdValueBase
    {
        public CommentId(Guid value)
            : base(value)
        {
        }
    }
}
