using MediaLakeCore.Application.Users.GetUsersList;
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
        [ProducesResponseType(typeof(IEnumerable<UsersListItemDto>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<UsersListItemDto>> GetAll()
        {
            var result = await _mediator.Send(new GetUsersListQuery());
            return result;
        }
    }
}
