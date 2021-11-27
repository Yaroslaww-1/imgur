using System;

namespace MediaLakeCore.Application.Posts.GetPostsList
{
    public class PostsListItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int CommentsCount { get; set; }
    }
}
