using MediaLakeCore.Application.Comments.CreateComment;
using MediaLakeCore.Application.Comments.GetCommentsList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaLakeCore.API.Controllers.Post.Comments
{
    [ApiController]
    [Route("posts/{postId}/comments")]
    public class CommentsController : ControllerBase
    {
        private readonly ISender _mediator;

        public CommentsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CommentsListItemDto>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<CommentsListItemDto>> GetList([FromRoute] Guid postId)
        {
            var result = await _mediator.Send(new GetCommentsListQuery(postId));
            return result;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<Guid> Create([FromRoute] Guid postId, [FromBody] CreateCommentRequest request)
        {
            var result = await _mediator.Send(new CreateCommentCommand(postId, request.Content));
            return result;
        }
    }
}
