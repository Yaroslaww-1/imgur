using System;
using System.Collections.Generic;

namespace MediaLakeCore.Domain.Users
{
    public class Role
    {
        public RoleId Id { get; private set; }
        public string Name { get; private set; }
        public List<User> Users { get; private set; }

        private Role()
        {
            // Only for EF
        }

        private Role(string name)
        {
            Id = new RoleId(Guid.NewGuid());
            Name = name;
            Users = new List<User>();
        }

        public static Role CreateNew(string name)
        {
            return new Role(name);
        }
    }
}
