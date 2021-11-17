using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediaLakeUsers.BuildingBlocks.ExecutionContext;
using MediaLakeUsers.BuildingBlocks.Security;
using MediaLakeUsers.Infrastructure.EntityFramework;
using MediaLakeUsers.Infrastructure.EntityFramework.Repositories.Users;
using MediaLakeUsers.Infrastructure.IdentityServer;
using MediaLakeUsers.Services.Auth.Dtos;
using MediaLakeUsers.Services.Users;

namespace MediaLakeUsers.Services.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly MediaLakeUsersDbContext _dbContext;
        private readonly ISecurityService _securityService;
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(MediaLakeUsersDbContext dbContext, ISecurityService securityService, IExecutionContextAccessor executionContextAccessor, IUserRepository userRepository)
        {
            _dbContext = dbContext;
            _securityService = securityService;
            _executionContextAccessor = executionContextAccessor;
            _userRepository = userRepository;
        }

        public async Task<AuthenticationResultDto> Authenticate(string login, string password)
        {
            var user = await _dbContext.Users
                .Include(u => u.Roles)
                .Where(u => u.Email == login)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return new AuthenticationResultDto()
                {
                    IsAuthenticated = false,
                    AuthenticationError = "Incorrect login or password"
                };
            }

            if (!_securityService.ValidatePassword(password, user.PasswordHash, user.PasswordHashSalt))
            {
                return new AuthenticationResultDto()
                {
                    IsAuthenticated = false,
                    AuthenticationError = "Incorrect login or password"
                };
            }

            var claims = new List<Claim>();
            claims.Add(new Claim(CustomClaimTypes.Name, user.Name));
            claims.Add(new Claim(CustomClaimTypes.Email, user.Email));
            claims.Add(new Claim(CustomClaimTypes.UserId, user.Id.ToString()));
            claims.Add(new Claim(CustomClaimTypes.Roles, JsonConvert.SerializeObject(user.Roles.Select(r => new RoleDto { Id = r.Id, Name = r.Name })), JsonClaimValueTypes.JsonArray));

            return new AuthenticationResultDto()
            {
                IsAuthenticated = true,
                AuthenticationError = null,
                User = new AuthenticatedUserDto()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    Roles = user.Roles.Select(r => new RoleDto() { Id = r.Id, Name = r.Name }).ToList(),
                    Claims = claims
                },
            };
        }

        public async Task<UserDto> GetAuthenticatedUser()
        {
            var authenticatedUserEmail = _executionContextAccessor.Email;

            var user = await _dbContext.Users
                .Include(u => u.Roles)
                .Where(u => u.Email == authenticatedUserEmail)
                .FirstOrDefaultAsync();

            return new UserDto()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Roles = user.Roles
                    .Select(r => new RoleDto() { Id = r.Id, Name = r.Name })
                    .ToList()
            };
        }
    }
}
