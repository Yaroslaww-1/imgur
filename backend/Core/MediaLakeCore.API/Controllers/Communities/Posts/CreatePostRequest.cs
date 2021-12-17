using System;
using System.Collections.Generic;

namespace MediaLakeCore.API.Controllers.Communities.Posts
{
    public class CreatePostRequest
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public List<Guid> ImagesIds { get; set; }
    }
}
