using MediaLakeCore.Domain.Comments.Events;
using MediaLakeCore.Domain.Posts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MediaLakeCore.Application.Comments.DomainEventHandlers
{
    public class CommentCreatedDomainEventHandler : INotificationHandler<CommentCreatedDomainEvent>
    {
        private readonly IPostRepository _postRepository;

        public CommentCreatedDomainEventHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task Handle(CommentCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var postOfComment = await _postRepository.GetByIdAsync(domainEvent.Comment.PostId);
            postOfComment.AddNewComment();
        }
    }
}
