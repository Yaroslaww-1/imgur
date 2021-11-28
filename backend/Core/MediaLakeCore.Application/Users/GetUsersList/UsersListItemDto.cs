using System;
using System.Collections.Generic;

namespace MediaLakeCore.Application.Users.GetUsersList
{
    public class UsersListItemRoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class UsersListItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<UsersListItemRoleDto> Roles { get; set; }
    }
}
