using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediaLakeUsers.Services.Auth;
using MediaLakeUsers.Services.Users;

namespace MediaLakeUsers.Controllers.AuthenticatedUser
{
    [ApiController]
    [Route("authenticatedUser")]
    public class AuthenticatedUserController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticatedUserController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        public async Task<UserDto> GetAuthenticatedUser()
        {
            var user = await _authenticationService.GetAuthenticatedUser();

            return user;
        }
    }
}
