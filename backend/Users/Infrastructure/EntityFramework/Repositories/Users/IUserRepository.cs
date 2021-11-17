using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaLakeUsers.Entities;

namespace MediaLakeUsers.Infrastructure.EntityFramework.Repositories.Users
{
    public interface IUserRepository
    {
        Task<IList<User>> GetAll();
        Task<User> GetById(Guid id);
        Task CreateUser(User user);
    }
}
