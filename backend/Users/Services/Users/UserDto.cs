using System;
using System.Collections.Generic;

namespace MediaLakeUsers.Services.Users
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public IList<RoleDto> Roles { get; set; }
    }
}
