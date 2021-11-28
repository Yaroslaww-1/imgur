using MediaLakeCore.Domain.Communities;
using MediaLakeCore.Domain.Users;
using System;

namespace MediaLakeCore.Domain.Posts
{
    public class Post
    {
        public PostId Id { get; private set; }
        public string Name { get; private set; }
        public string Content { get; private set; }
        public CommunityId CommunityId { get; private set; }
        public User CreatedBy { get; private set; }
        public DateTime CreatedAt { get; private set; }

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
        }

        public static Post CreateNew(CommunityId communityId, string name, string content, User createdBy)
        {
            return new Post(communityId, name, content, createdBy);
        }
    }
}
