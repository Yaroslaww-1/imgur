using System;

namespace MediaLakeCore.Application.Posts.Dtos
{
    public class PostByIdCreatedByDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class PostByIdDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int CommentsCount { get; set; }
        public PostByIdCreatedByDto CreatedBy { get; set; }
    }
}
