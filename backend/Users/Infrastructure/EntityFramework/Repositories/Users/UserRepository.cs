using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaLakeUsers.Entities;

namespace MediaLakeUsers.Infrastructure.EntityFramework.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly UsersDbContext _dbContext;

        public UserRepository(UsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<User>> GetAll()
        {
            var users = await _dbContext.Users
                .Include(u => u.Roles)
                .ToListAsync();

            return users;
        }

        public async Task<User> GetById(Guid id)
        {
            var user = await _dbContext.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task CreateUser(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
