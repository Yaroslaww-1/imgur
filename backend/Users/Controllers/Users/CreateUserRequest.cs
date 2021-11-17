using System.Collections.Generic;

namespace MediaLakeUsers.Controllers.Users
{
    public class CreateUserRequest
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }
    }
}
