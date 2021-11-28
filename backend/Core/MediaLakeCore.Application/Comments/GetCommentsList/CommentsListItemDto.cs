using System;

namespace MediaLakeCore.Application.Comments.GetCommentsList
{
    public class CommentsListItemCreatedByDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class CommentsListItemDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public CommentsListItemCreatedByDto CreatedBy { get; set; }
    }
}
