
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;
using AppDiv.SmartAgency.Utility.Contracts;
/*
namespace AppDiv.SmartAgency.Application.Features.Applicants.Query
{
    public record GetApplicantCvAttachmentQuery(Guid Id, string folderName) : IRequest<FileResponse>
    {

    }

    public class GetApplicantCvAttachmentHandler : IRequestHandler<GetApplicantCvAttachmentQuery, FileResponse>
    {
        private readonly IFileService _fileService;

        public GetApplicantCvAttachmentHandler(IFileService fileService)
        {
            _fileService = fileService;
        }
        public async Task<FileResponse> Handle(GetApplicantCvAttachmentQuery request, CancellationToken cancellationToken)
        {

            var fileResponse = new FileResponse();

            var res = _fileService.getFile(request.Id.ToString(), request.folderName, null);
            fileResponse.File = Convert.ToBase64String(res.file);
            fileResponse.FileName = res.fileName;
            fileResponse.FileExtension = res.fileExtenion;

            return fileResponse;

        }

    }
}*/