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

        //[HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<PostCommentDto>), StatusCodes.Status200OK)]
        //public async Task<PostForListDto> Get([FromBody] CreatePostCommentRequest request, [FromRoute] Guid postId)
        //{
        //    var result = await _mediator.Send(new CreatePostCommentCommand(request.Name, request.Content));
        //    return result;
        //}

        //[HttpPost]
        //[ProducesResponseType(typeof(PostDto), StatusCodes.Status200OK)]
        //public async Task<PostDto> Create([FromBody] CreatePostCommentRequest request, [FromRoute] Guid postId)
        //{
        //    var result = await _mediator.Send(new CreatePostCommentCommand(request.Name, request.Content));
        //    return result;
        //}
    }
}
