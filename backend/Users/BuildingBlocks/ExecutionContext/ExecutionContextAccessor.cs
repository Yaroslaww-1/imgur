using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace MediaLakeUsers.BuildingBlocks.ExecutionContext
{
    public class ExecutionContextAccessor : IExecutionContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExecutionContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Email
        {
            get
            {
                //http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier
                if (_httpContextAccessor
                    .HttpContext?
                    .User?
                    .Claims?
                    .SingleOrDefault(x => x.Type.Contains("email"))?
                    .Value != null)
                {
                    return _httpContextAccessor.HttpContext.User.Claims.Single(
                        x => x.Type.Contains("email")).Value;
                }

                if ((bool)(_httpContextAccessor
                    .HttpContext?
                    .Request?
                    .Headers?
                    .ContainsKey("email")))
                {
                    return _httpContextAccessor
                        .HttpContext?
                        .Request?
                        .Headers?
                        ["email"];
                }

                throw new ApplicationException("User context is not available");
            }
        }

        public bool IsAvailable => _httpContextAccessor.HttpContext != null;
    }
}
