using MediaLakeCore.Application.Posts.CreatePost;
using MediaLakeCore.Application.Posts.Dtos;
using MediaLakeCore.Application.Posts.GetPostsById;
using MediaLakeCore.Application.Posts.GetPostsList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PostsListItemDto>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<PostsListItemDto>> GetList()
        {
            var result = await _mediator.Send(new GetPostsListQuery());
            return result;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PostByIdDto), StatusCodes.Status200OK)]
        public async Task<PostByIdDto> GetById([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetPostByIdQuery(id));
            return result;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<Guid> Create([FromBody] CreatePostRequest request)
        {
            var result = await _mediator.Send(new CreatePostCommand(request.Name, request.Content));
            return result;
        }
    }
}
