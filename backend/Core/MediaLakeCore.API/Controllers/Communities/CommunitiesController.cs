using MediaLakeCore.Application.Communities.CreateCommunity;
using MediaLakeCore.Application.Communities.GetCommunitiesList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaLakeCore.API.Controllers.Communities
{
    [ApiController]
    [Route("communities")]
    public class CommunitiesController : ControllerBase
    {
        private readonly ISender _mediator;

        public CommunitiesController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CommunitiesListItemDto>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<CommunitiesListItemDto>> GetList()
        {
            var result = await _mediator.Send(new GetCommunitiesListQuery());
            return result;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<Guid> Create([FromBody] CreateCommunityRequest request)
        {
            var result = await _mediator.Send(new CreateCommunityCommand(request.Name, request.Description));
            return result;
        }
    }
}
