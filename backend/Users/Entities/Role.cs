using System;
using System.Collections.Generic;

namespace MediaLakeUsers.Entities
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<User> Users { get; set; }

        private Role()
        {
            // Only for EF
        }

        private Role(Guid id, string name)
        {
            Id = id;
            Name = name;
            Users = new List<User>();
        }

        private Role(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Users = new List<User>();
        }

        public static Role InitializeExisting(Guid id, string name)
        {
            return new Role(id, name);
        }

        public static Role CreateNew(string name)
        {
            return new Role(name);
        }
    }
}
