using MediaLakeCore.BuildingBlocks.Domain;

namespace MediaLakeCore.Domain.Comments.Events
{
    public class CommentCreatedDomainEvent : DomainEventBase
    {
        public CommentCreatedDomainEvent(Comment comment)
        {
            Comment = comment;
        }

        public Comment Comment { get; }
    }
}
