using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace MediaLakeCore.BuildingBlocks.ExecutionContext
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
                if ((bool)(_httpContextAccessor
                    .HttpContext?
                    .Request?
                    .Headers?
                    .ContainsKey("UserEmail")))
                {
                    return _httpContextAccessor
                        .HttpContext?
                        .Request?
                        .Headers?
                        ["UserEmail"];
                }

                throw new ApplicationException("User context is not available");
            }
        }

        public bool IsAvailable => _httpContextAccessor.HttpContext != null;
    }
}
