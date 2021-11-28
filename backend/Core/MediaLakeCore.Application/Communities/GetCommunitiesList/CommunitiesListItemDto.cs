using System;

namespace MediaLakeCore.Application.Communities.GetCommunitiesList
{
    public class CommunitiesListItemCreatedByDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class CommunitiesListItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CommunitiesListItemCreatedByDto CreatedBy { get; set; }
    }
}
