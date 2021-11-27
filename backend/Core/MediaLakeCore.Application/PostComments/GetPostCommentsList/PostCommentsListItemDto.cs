using System;

namespace MediaLakeCore.Application.PostComments.GetPostCommentsList
{
    public class PostCommentsListItemCreatedByDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class PostCommentsListItemDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public PostCommentsListItemCreatedByDto CreatedBy { get; set; }
    }
}
