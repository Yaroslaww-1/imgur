using MediaLakeCore.Application.Posts.CreatePost;
using MediaLakeCore.Application.Posts.Dtos;
using MediaLakeCore.Application.Posts.GetChats;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaLakeCore.API.Controllers.Posts
{
    [ApiController]
    [Route("posts")]
    public class PostsController : ControllerBase
    {
        private readonly ISender _mediator;

        public PostsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("mine")]
        [ProducesResponseType(typeof(IEnumerable<PostDto>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<PostDto>> Create()
        {
            var result = await _mediator.Send(new GetAuthenticatedUserPostsQuery());
            return result;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PostDto), StatusCodes.Status200OK)]
        public async Task<PostDto> Create([FromBody] CreatePostRequest request)
        {
            var result = await _mediator.Send(new CreatePostCommand(request.Name, request.Content));
            return result;
        }
    }
}
