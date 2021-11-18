using System;
using System.Collections.Generic;

namespace MediaLakeCore.Application.Users.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<RoleDto> Roles { get; set; }
    }
}
