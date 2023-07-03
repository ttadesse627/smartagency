using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Features.PostImages.Command.Create;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Utility.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers
{
    [ApiController]
    [Route("api/postImage")]
    public class PostImageController : ControllerBase
    {
        private IMediator _mediator;
        private IFileService _fileService;
        public PostImageController(IMediator mediator, IFileService fileService)
        {
            _mediator = mediator;
            _fileService = fileService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<string>> CreatePostImage(CreatePostImageCommand postImageRequest, CancellationToken token)
        {
            var response = await _mediator.Send(postImageRequest);
            return Ok(response);
        }

        [HttpGet("get-image")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] string id)
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


            // return File(response.file,
            //     "application/octet-stream"
            //    , response.fileName+response.fileExtenion);

        }

        [HttpGet("get-all-sliders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<String> DeleteSlider([FromQuery] string id)
        {
            try
            {
                var folderName = "Sliders";
                string fileName = id + "*"; // Replace "." with the actual file extension
                var response = _fileService.DeleteFile(fileName, folderName);
                return response;


                // return NoContent();
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