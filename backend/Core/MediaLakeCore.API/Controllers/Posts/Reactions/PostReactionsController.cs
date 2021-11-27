using MediaLakeCore.Application.PostReactions.TogglePostDislike;
using MediaLakeCore.Application.PostReactions.TogglePostLike;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MediaLakeCore.API.Controllers.Posts.Reactions
{
    [ApiController]
    [Route("posts/{postId}/reactions")]
    public class PostReactionsController : ControllerBase
    {
        private readonly ISender _mediator;

        public PostReactionsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("toggleLike")]
        [ProducesResponseType(typeof(TogglePostLikeDto), StatusCodes.Status200OK)]
        public async Task<TogglePostLikeDto> ToggleLike([FromRoute] Guid postId)
        {
            var result = await _mediator.Send(new TogglePostLikeCommand(postId));
            return result;
        }

        [HttpPost("toggleDislike")]
        [ProducesResponseType(typeof(TogglePostDislikeDto), StatusCodes.Status200OK)]
        public async Task<TogglePostDislikeDto> ToggleDislike([FromRoute] Guid postId)
        {
            var result = await _mediator.Send(new TogglePostDislikeCommand(postId));
            return result;
        }
    }
}
