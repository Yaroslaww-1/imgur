using MediaLakeCore.Domain.Users;
using System;

namespace MediaLakeCore.Domain.Communities
{
    public class Community
    {
        public CommunityId Id { get; private set; }
        public User CreatedBy { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        private Community()
        {
            // Only for EF
        }

        private Community(CommunityId id, User createdBy, string name, string description)
        {
            Id = id;
            CreatedBy = createdBy;
            Name = name;
            Description = description;
        }

        private Community(User createdBy, string name, string description)
        {
            Id = new CommunityId(Guid.NewGuid());
            CreatedBy = createdBy;
            Name = name;
            Description = description;
        }

        public static Community Initialize(CommunityId communityId, User createdBy, string name, string description)
        {
            return new Community(communityId, createdBy, name, description);
        }

        public static Community CreateNew(User createdBy, string name, string description)
        {
            return new Community(createdBy, name, description);
        }
    }
}
