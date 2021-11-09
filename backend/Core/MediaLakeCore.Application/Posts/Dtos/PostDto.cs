using System;
using System.Collections.Generic;

namespace MediaLakeCore.Application.Posts.Dtos
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public List<PostCommentDto> Comments { get; set; }
    }
}
