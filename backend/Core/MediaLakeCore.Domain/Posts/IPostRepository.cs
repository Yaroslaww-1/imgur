using System.Threading.Tasks;

namespace MediaLakeCore.Domain.Posts
{
    public interface IPostRepository
    {
        public Task AddAsync(Post post);
    }
}
