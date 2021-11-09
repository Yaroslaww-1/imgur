using MediaLakeCore.Application.Users.Dtos;
using MediaLakeCore.Application.Users.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaLakeCore.API.Users.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly ISender _mediator;

        public UsersController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var result = await _mediator.Send(new GetUsersQuery());
            return result;
        }
    }
}
