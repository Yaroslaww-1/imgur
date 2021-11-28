using MediaLakeCore.Domain.Communities;
using System;
using System.Collections.Generic;

namespace MediaLakeCore.Domain.Users
{
    public class User
    {
        public UserId Id { get; private set; }
        public string Email { get; private set; }
        public string Name { get; private set; }
        public List<Role> Roles { get; private set; }

        private User()
        {
            // Only for EF
        }

        private User(
            UserId id,
            string email,
            string name,
            List<Role> roles)
        {
            Id = id;
            Email = email;
            Name = name;
            Roles = roles;
        }

        private User(string email, string name, List<Role> roles)
        {
            Id = new UserId(Guid.NewGuid());
            Email = email;
            Name = name;
            Roles = roles;
        }

        public static User CreateNew(string email, string name, List<Role> roles)
        {
            return new User(email, name, roles);
        }

        public static User Initialize(
            UserId id,
            string email,
            string name,
            List<Role> roles)
        {
            return new User(id, email, name, roles);
        }
    }
}
