using MediaLakeCore.Application.Posts.Dtos;
using MediaLakeCore.Application.Posts.GetAuthenticatedUserPostsList;
using MediaLakeCore.Application.Posts.GetPostsById;
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

        [HttpGet("authenticatedUser")]
        [ProducesResponseType(typeof(IEnumerable<AuthenticatedUserPostsListItemDto>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<AuthenticatedUserPostsListItemDto>> GetList()
        {
            var result = await _mediator.Send(new GetAuthenticatedUserPostsListQuery());
            return result;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PostByIdDto), StatusCodes.Status200OK)]
        public async Task<PostByIdDto> GetById([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetPostByIdQuery(id));
            return result;
        }
    }
}
