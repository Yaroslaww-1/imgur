using System.Threading.Tasks;

namespace MediaLakeCore.Domain.PostComments
{
    public interface IPostCommentRepository
    {
        public Task AddAsync(PostComment comment);
    }
}
