using MediaLakeCore.BuildingBlocks.Domain;
using MediaLakeCore.Domain.Communities;
using MediaLakeCore.Domain.Users;
using System;

namespace MediaLakeCore.Domain.Posts
{
    public class Post : Entity, IAggregateRoot
    {
        public PostId Id { get; private set; }
        public string Name { get; private set; }
        public string Content { get; private set; }
        public CommunityId CommunityId { get; private set; }
        public User CreatedBy { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public int CommentsCount { get; private set; }
        public int LikesCount { get; private set; }
        public int DislikesCount { get; private set; }

        private Post()
        {
            // Only for EF
        }

        private Post(CommunityId communityId, string name, string content, User createdBy)
        {
            Id = new PostId(Guid.NewGuid());
            CommunityId = communityId;
            Name = name;
            Content = content;
            CreatedBy = createdBy;
            CreatedAt = DateTime.Now;
            CommentsCount = 0;
            LikesCount = 0;
            DislikesCount = 0;
        }

        internal static Post CreateNew(CommunityId communityId, string name, string content, User createdBy)
        {
            return new Post(communityId, name, content, createdBy);
        }

        public void AddNewLike() => LikesCount++;
        public void RemoveExistingLike() => LikesCount--;

        public void AddNewDislike() => DislikesCount++;
        public void RemoveExistingDislike() => DislikesCount--;

        public void AddNewComment() => CommentsCount++;
    }
}
