
using AppDiv.SmartAgency.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace AppDiv.SmartAgency.API.Controllers
{
    public class FileController : ApiControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] Guid id, [FromQuery] string fileType)
        {
            var response = _fileService.getFile(id.ToString(), fileType);

            return File(response.file,
                            "application/octet-stream"
                            , response.fileName + response.fileExtenion);
        }

    }
}