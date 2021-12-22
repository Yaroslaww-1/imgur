using System;

namespace MediaLakeCore.Application.Communities.GetCommunityById
{
    public class CreatedByDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class CommunityByIdDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MembersCount { get; set; }
        public bool IsAuthenticatedUserJoined { get; set; }
        public CreatedByDto CreatedBy { get; set; } = null!;
    }
}