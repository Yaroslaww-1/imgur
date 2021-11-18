using MediaLakeCore.Application.PostComments.CreatePostComment;
using MediaLakeCore.Application.Posts.CreatePost;
using MediaLakeCore.Application.Posts.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaLakeCore.API.Controllers.Post.Comments
{
    [ApiController]
    [Route("posts/{postId}/comments")]
    public class PostCommentsController : ControllerBase
    {
        private readonly ISender _mediator;

        public PostCommentsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<Guid> Create([FromRoute] Guid postId, [FromBody] CreatePostCommentRequest request)
        {
            var result = await _mediator.Send(new CreatePostCommentCommand(postId, request.Content));
            return result;
        }
    }
}
