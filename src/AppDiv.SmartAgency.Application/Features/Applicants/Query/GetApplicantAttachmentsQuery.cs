using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs.ApplicantsCvDTOs;
using AppDiv.SmartAgency.Utility.Exceptions;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Query
{
    public record GetApplicantAttachmentsQuery(Guid ApplicantId, string AttachmentType) : IRequest<string> { }

    public class GetApplicantAttachmentsQueryHandler(IFileService fileService) : IRequestHandler<GetApplicantAttachmentsQuery, string>
    {
        private readonly IFileService _fileService = fileService;

        public async Task<string> Handle(GetApplicantAttachmentsQuery query, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            List<string> fields = [];
            if (query.ApplicantId == null)
            {
                fields.Add(query.ApplicantId.ToString());
            }

            if (string.IsNullOrEmpty(query.AttachmentType))
            {
                fields.Add(query.AttachmentType);
            }
            if (fields.Count > 0)
            {
                throw new RequiredFeildException("Null Fields", fields);
            }
            string? response = Convert.ToBase64String(_fileService.getFile(query.ApplicantId.ToString(), query.AttachmentType, null).file);

            return response;
        }

    }
}