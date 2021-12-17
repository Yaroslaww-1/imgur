using MediaLakeCore.Application.Posts.DeletePostImage;
using MediaLakeCore.Application.Posts.UploadPostImage;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MediaLakeCore.API.Controllers.Posts.Images
{
    [ApiController]
    [Route("posts/images")]
    public class PostImagesController : ControllerBase
    {
        private readonly ISender _mediator;

        public PostImagesController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PostImageDto), StatusCodes.Status200OK)]
        public async Task<PostImageDto> Upload([FromBody] UploadPostImageRequest request)
        {
            var result = await _mediator.Send(new UploadPostImageCommand(request.Content));
            return result;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _mediator.Send(new DeletePostImageCommand(id));
            return Ok();
        }
    }
}
