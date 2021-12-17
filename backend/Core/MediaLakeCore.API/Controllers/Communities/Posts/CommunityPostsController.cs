using MediaLakeCore.Application.Communities.GetCommunityPosts;
using MediaLakeCore.Application.Posts.CreatePost;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaLakeCore.API.Controllers.Communities.Posts
{
    [ApiController]
    [Route("communities/{communityId}/posts")]
    public class CommunityPostsController : ControllerBase
    {
        private readonly ISender _mediator;

        public CommunityPostsController(ISender mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CommunityPostsListItemDto>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<CommunityPostsListItemDto>> GetPosts([FromRoute] Guid communityId)
        {
            var result = await _mediator.Send(new GetCommunityPostsQuery(communityId));
            return result;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<Guid> Create([FromRoute] Guid communityId, [FromBody] CreatePostRequest request)
        {
            var result = await _mediator.Send(new CreatePostCommand(communityId, request.Name, request.Content, request.ImagesIds));
            return result;
        }
    }
}
