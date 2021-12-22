using System;

namespace MediaLakeCore.Application.Communities.GetAuthenticatedUserCommunities
{
    public class CreatedByDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class AuthenticatedUserCommunitiesListItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MembersCount { get; set; }
        public CreatedByDto CreatedBy { get; set; } = null!;
    }
}
