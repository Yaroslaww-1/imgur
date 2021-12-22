using MediaLakeCore.Application.Communities.CreateCommunity;
using MediaLakeCore.Application.Communities.GetAuthenticatedUserCommunities;
using MediaLakeCore.Application.Communities.GetCommunitiesList;
using MediaLakeCore.Application.Communities.JoinCommunity;
using MediaLakeCore.Application.Communities.LeaveCommunity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaLakeCore.Application.Communities.GetCommunityById;

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

        [HttpGet("authenticatedUser")]
        [ProducesResponseType(typeof(IEnumerable<AuthenticatedUserCommunitiesListItemDto>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<AuthenticatedUserCommunitiesListItemDto>> GetAuthenticatedUserList()
        {
            var result = await _mediator.Send(new GetAuthenticatedUserCommunitiesQuery());
            return result;
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CommunityByIdDto), StatusCodes.Status200OK)]
        public async Task<CommunityByIdDto> GetById([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetCommunityByIdQuery(id));
            return result;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<Guid> Create([FromBody] CreateCommunityRequest request)
        {
            var result = await _mediator.Send(new CreateCommunityCommand(request.Name, request.Description));
            return result;
        }

        [HttpPost("{communityId}/join")]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        public async Task<ActionResult> Join([FromRoute] Guid communityId)
        {
            await _mediator.Send(new JoinCommunityCommand(communityId));
            return Ok();
        }

        [HttpPost("{communityId}/leave")]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        public async Task<ActionResult> Leave([FromRoute] Guid communityId)
        {
            await _mediator.Send(new LeaveCommunityCommand(communityId));
            return Ok();
        }
    }
}
