
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.PostImages.Command.Create
{
    public class CreatePostImageCommandHandler : IRequestHandler<CreatePostImageCommand, string>
    {
        private readonly IFileService _fileService;

        public CreatePostImageCommandHandler(IFileService fileService)
        {
            _fileService = fileService;
        }
        public async Task<string> Handle(CreatePostImageCommand command, CancellationToken cancellationToken)
        {
            var response = new ServiceResponse<Int32>();

            var fileModels = new List<FileModel>();
            // save order attachment
            var folderName = Path.Combine("Resources", "Sliders");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            foreach (var image in command.Request.SliderImages)
            {
                if (!string.IsNullOrEmpty(image))
                {
                    var fileModel = new FileModel
                    {
                        Base64String = image,
                        FileName = Guid.NewGuid().ToString(),
                        PathToSave = pathToSave,
                        FileMode = FileMode.Create
                    };
                    fileModels.Add(fileModel);
                }
            }
            var fileSaved = await _fileService.UpLoadMultipleFilesAsync(fileModels);
            if (!fileSaved)
            {
                response.Errors?.Add("Couldn't save slider images.");
            }
            return "sucessfully inserted";
        }
    }
}
