using System.Threading.Tasks;

namespace MediaLakeCore.Domain.Users
{
    public interface IUserRepository
    {
        public Task AddAsync(User user);
        public Task<User> GetByIdAsync(UserId userId);
    }
}
