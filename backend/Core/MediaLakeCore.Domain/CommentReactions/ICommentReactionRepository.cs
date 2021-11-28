using System.Threading.Tasks;

namespace MediaLakeCore.Domain.CommentReactions
{
    public interface ICommentReactionRepository
    {
        public Task AddAsync(CommentReaction reaction);
        public void Add(CommentReaction reaction);
        public void Delete(CommentReaction reaction);
    }
}
