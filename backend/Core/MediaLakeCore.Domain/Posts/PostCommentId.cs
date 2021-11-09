using MediaLakeCore.BuildingBlocks.Domain;
using System;

namespace MediaLakeCore.Domain.Posts
{
    public class PostCommentId : TypedIdValueBase
    {
        public PostCommentId(Guid value)
            : base(value)
        {
        }
    }
}
