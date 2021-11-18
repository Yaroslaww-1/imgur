using MediaLakeUsers.Services.Users;

namespace MediaLakeUsers.Services.Auth.Dtos
{
    public class AuthenticationResultDto
    {
        public bool IsAuthenticated { get; set; }

        public string AuthenticationError { get; set; }

        public AuthenticatedUserDto User { get; set; }
    }
}
