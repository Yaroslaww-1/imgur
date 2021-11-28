using System.Threading.Tasks;

namespace MediaLakeCore.Domain.Comments
{
    public interface ICommentRepository
    {
        public Task AddAsync(Comment comment);
    }
}
