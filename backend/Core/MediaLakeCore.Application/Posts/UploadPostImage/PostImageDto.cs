using System;

namespace MediaLakeCore.Application.Posts.UploadPostImage
{
    public class PostImageDto
    {
        public Guid Id { get; set; }
        public string Url { get; set; } = null!;
    }
}
