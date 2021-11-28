using MediaLakeCore.Application.CommentReactions.ToggleCommentDislike;
using MediaLakeCore.Application.CommentReactions.ToggleCommentLike;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MediaLakeCore.API.Controllers.Posts.Comments.Reactions
{
    [ApiController]
    [Route("posts/{postId}/comments/{commentId}")]
    public class CommentReactionsController : ControllerBase
    {
        private readonly ISender _mediator;

        public CommentReactionsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("toggleLike")]
        [ProducesResponseType(typeof(ToggleCommentLikeDto), StatusCodes.Status200OK)]
        public async Task<ToggleCommentLikeDto> ToggleLike([FromRoute] Guid postId, [FromRoute] Guid commentId)
        {
            var result = await _mediator.Send(new ToggleCommentLikeCommand(postId, commentId));
            return result;
        }

        [HttpPost("toggleDislike")]
        [ProducesResponseType(typeof(ToggleCommentDislikeDto), StatusCodes.Status200OK)]
        public async Task<ToggleCommentDislikeDto> ToggleDislike([FromRoute] Guid postId, [FromRoute] Guid commentId)
        {
            var result = await _mediator.Send(new ToggleCommentDislikeCommand(postId, commentId));
            return result;
        }
    }
}
