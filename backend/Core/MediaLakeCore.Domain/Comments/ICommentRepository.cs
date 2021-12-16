using System.Threading.Tasks;

namespace MediaLakeCore.Domain.Comments
{
    public interface ICommentRepository
    {
        public Task<Comment> GetByIdAsync(CommentId commentId);
        public Task AddAsync(Comment comment);
    }
}
