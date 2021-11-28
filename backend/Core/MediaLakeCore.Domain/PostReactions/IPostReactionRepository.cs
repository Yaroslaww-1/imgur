using System.Threading.Tasks;

namespace MediaLakeCore.Domain.PostReactions
{
    public interface IPostReactionRepository
    {
        public Task AddAsync(PostReaction reaction);
        public void Add(PostReaction reaction);
        public void Delete(PostReaction reaction);
    }
}
