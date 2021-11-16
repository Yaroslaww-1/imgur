using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaLakeUsers.BuildingBlocks.Security;
using MediaLakeUsers.Entities;
using MediaLakeUsers.Infrastructure.EntityFramework;
using MediaLakeUsers.Infrastructure.EntityFramework.Repositories.Users;
using MediaLakeUsers.Infrastructure.EventBus.Integration;
using MediaLakeUsers.Services.Users.IntegrationEvents;
using Microsoft.EntityFrameworkCore;

namespace MediaLakeUsers.Services.Users
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISecurityService _securityService;
        private readonly IIntegrationEventBus _integrationEventBus;
        private readonly MediaLakeUsersDbContext _dbContext;

        public UserService(IUserRepository userRepository, ISecurityService securityService, IIntegrationEventBus integrationEventBus, MediaLakeUsersDbContext dbContext)
        {
            _userRepository = userRepository;
            _securityService = securityService;
            _integrationEventBus = integrationEventBus;
            _dbContext = dbContext;
        }

        public async Task<IList<UserDto>> GetAllUsers()
        {
            return (await _userRepository.GetAll())
                .Select(u => new UserDto()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Roles = u.Roles
                        .Select(r => new RoleDto() { Id = r.Id, Name = r.Name })
                        .ToList()
                })
                .ToList();
        }

        public async Task InitializeUser(
            Guid id,
            string email,
            string name,
            string password,
            List<Role> roles)
        {
            var user = User.CreateNew(
                email,
                name,
                password,
                roles,
                _securityService);

            user.Id = id;

            await _userRepository.CreateUser(user);

            await _integrationEventBus.Publish(new UserCreatedIntegrationEvent(user.Id, user.Name, user.Email, roles.Select(r => r.Name).ToList()));
        }

        public async Task<Guid> CreateUser(
            string email,
            string name,
            string password,
            List<string> roles)
        {
            var userRoles = await _dbContext.Roles.Where(r => roles.Contains(r.Name)).ToListAsync();

            var user = User.CreateNew(
                email,
                name,
                password,
                userRoles,
                _securityService);

            await _userRepository.CreateUser(user);

            await _integrationEventBus.Publish(new UserCreatedIntegrationEvent(user.Id, user.Name, user.Email, user.Roles.Select(r => r.Name).ToList()));

            return user.Id;
        }
    }
}
