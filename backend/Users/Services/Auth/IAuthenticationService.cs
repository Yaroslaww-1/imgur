using System.Threading.Tasks;
using MediaLakeUsers.Services.Auth.Dtos;
using MediaLakeUsers.Services.Users;

namespace MediaLakeUsers.Services.Auth
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResultDto> Authenticate(string login, string password);
        Task<UserDto> GetAuthenticatedUser();
    }
}
