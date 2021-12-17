using Ardalis.Specification;
using MediaLakeCore.Domain.PostImages;
using System.Collections.Generic;
using System.Linq;

namespace MediaLakeCore.Application.Posts.Specifications
{
    public class PostImagesByIdsSpecification : Specification<PostImage>
    {
        public PostImagesByIdsSpecification(List<PostImageId> postImagesIds)
        {
            base.Query.Where(pi => postImagesIds.ToArray().Contains(pi.Id));
        }
    }
}
