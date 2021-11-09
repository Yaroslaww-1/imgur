using System;
using System.Collections.Generic;
using MediaLakeUsers.BuildingBlocks.Security;

namespace MediaLakeUsers.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordHashSalt { get; set; }
        public IList<Role> Roles { get; set; }

        private User()
        {
            // Only for EF
        }

        private User(
            string email,
            string name,
            string passwordHash,
            string passwordHashSalt,
            List<Role> roles)
        {
            Id = Guid.NewGuid();
            Email = email;
            Name = name;
            PasswordHash = passwordHash;
            PasswordHashSalt = passwordHashSalt;
            Roles = roles;
        }

        public static User CreateNew(string email,
            string name,
            string password,
            List<Role> roles,
            ISecurityService securityService)
        {
            var salt = securityService.GetRandomSalt();

            var passwordHash = securityService.HashPassword(password, salt);

            return new User(email, name, passwordHash, Convert.ToBase64String(salt), roles);
        }
    }
}
