using System;

namespace MediaLakeCore.Application.Posts.Dtos
{
    public class PostForListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int CommentsCount { get; set; }
    }
}
