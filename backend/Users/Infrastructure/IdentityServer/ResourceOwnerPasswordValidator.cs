using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using MediaLakeUsers.Services.Auth;

namespace MediaLakeUsers.Infrastructure.IdentityServer
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IAuthenticationService _authenticationService;

        public ResourceOwnerPasswordValidator(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var authenticationResult = await _authenticationService.Authenticate(context.UserName, context.Password);

            if (!authenticationResult.IsAuthenticated)
            {
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant,
                    authenticationResult.AuthenticationError);

                return;
            }

            context.Result = new GrantValidationResult(
                authenticationResult.User.Id.ToString(),
                "forms",
                authenticationResult.User.Claims);
        }
    }
}
