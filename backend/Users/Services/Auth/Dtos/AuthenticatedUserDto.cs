using System;
using System.Collections.Generic;
using System.Security.Claims;
using MediaLakeUsers.Services.Users;

namespace MediaLakeUsers.Services.Auth.Dtos
{
    public class AuthenticatedUserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public IList<RoleDto> Roles { get; set; }
        public List<Claim> Claims { get; set; }
    }
}
