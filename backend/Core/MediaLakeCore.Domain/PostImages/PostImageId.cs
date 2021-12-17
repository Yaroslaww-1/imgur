using MediaLakeCore.BuildingBlocks.Domain;
using System;

namespace MediaLakeCore.Domain.PostImages
{
    public class PostImageId : TypedIdValueBase
    {
        public PostImageId(Guid value)
            : base(value)
        {
        }
    }
}
