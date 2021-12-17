using System.Threading.Tasks;

namespace MediaLakeCore.Domain.PostImages
{
    public interface IPostImageRepository
    {
        public Task AddAsync(PostImage postImage);
        public Task<PostImage> GetByIdAsync(PostImageId postImageId);
        public Task DeleteAsync(PostImage postImage);
    }
}
