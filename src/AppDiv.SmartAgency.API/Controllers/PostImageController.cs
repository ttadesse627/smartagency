using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Pagess;
using AppDiv.SmartAgency.Application.Features.PostImages.Command.Create;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Utility.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers
{
    public class PostImageController(IMediator mediator, IFileService fileService) : ApiControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IFileService _fileService = fileService;

        [HttpPost("create")]
        public async Task<ActionResult<ServiceResponse<Int32>>> CreatePostImage(CreatePostImageRequest postImageRequest, CancellationToken token)
        {
            var response = await _mediator.Send(new CreatePostImageCommand(postImageRequest), token);
            return Ok(response);
        }

        [HttpGet("get-image")]
        public IActionResult Get([FromQuery] string id)
        {
            var postImageId = id;
            // var response = _fileService.getFile(id.ToString(),fileType , folderType);

            try
            {
                var fileType = "sliders";

                //string fileName = "Slider" + id.ToString() + ".jpg"; // Replace ".jpg" with the actual file extension
                (byte[], string, string) fileResult = _fileService.getFile(postImageId, fileType, null);

                //(byte[], string, string) fileResult = _fileService.GetFile(fileName, "Sliders");

                // Convert the byte array of the image content to a Base64 encoded string
                string fileContent = Convert.ToBase64String(fileResult.Item1);

                // Create an anonymous object with properties "FileName" and "FileContent"
                var response = new { FileName = fileResult.Item2 + fileResult.Item3, FileContent = fileContent };

                // Return the anonymous object as a JSON response
                return Ok(response);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet("get-all-sliders")]
        public IActionResult GetAllSliders()
        {
            try
            {
                var folderType = "sliders";
                List<(byte[], string, string)> sliderImages = _fileService.GetAllImages(folderType);

                // Create a list of anonymous objects with properties "FileName" and "FileContent"
                var response = sliderImages.Select(x => new { FileName = x.Item2, FileContent = Convert.ToBase64String(x.Item1) });

                // Return the list of anonymous objects as a JSON response
                return Ok(response);

            }

            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("delete-slider")]
        public ActionResult<String> DeleteSlider([FromQuery] string id)
        {
            try
            {
                var folderName = "Sliders";
                string fileName = id + "*"; // Replace "." with the actual file extension
                var response = _fileService.DeleteFile(fileName, folderName);
                return response;
            }

            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }

            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}