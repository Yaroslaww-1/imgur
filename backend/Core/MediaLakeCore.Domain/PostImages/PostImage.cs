using MediaLakeCore.BuildingBlocks.Domain;
using MediaLakeCore.Domain.Posts;
using System;

namespace MediaLakeCore.Domain.PostImages
{
    public class PostImage : Entity, IAggregateRoot
    {
        public PostImageId Id { get; private set; }
#nullable enable
        public PostId? PostId { get; private set; }
#nullable disable
        public string Url { get; private set; }
        public PostImageStatus Status { get; private set; }

        private PostImage()
        {
            // Only for EF
        }

        private PostImage(PostId postId, string url)
        {
            Id = new PostImageId(Guid.NewGuid());
            PostId = postId;
            Url = url;
            Status = PostImageStatus.UsedInCreatedPost;
        }

        private PostImage(string url)
        {
            Id = new PostImageId(Guid.NewGuid());
            Url = url;
            Status = PostImageStatus.Draft;
        }

        public static PostImage CreateNewDraft(string url)
        {
            return new PostImage(url);
        }

        public void MarkUsed(PostId postId)
        {
            PostId = postId;
            Status = PostImageStatus.UsedInCreatedPost;
        }
    }
}
