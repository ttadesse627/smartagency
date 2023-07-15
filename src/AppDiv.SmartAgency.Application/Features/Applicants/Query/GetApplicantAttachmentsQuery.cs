using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs.ApplicantsCvDTOs;
using AppDiv.SmartAgency.Utility.Exceptions;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Query
{
    public record GetApplicantAttachmentsQuery(Guid ApplicantId, string AttachmentType) : IRequest<string> { }

    public class GetApplicantAttachmentsQueryHandler : IRequestHandler<GetApplicantAttachmentsQuery, string>
    {
        private readonly IApplicantRepository _applicantRepository;
        private readonly IFileService _fileService;

        public GetApplicantAttachmentsQueryHandler(IApplicantRepository applicantRepository, IFileService fileService)
        {
            _applicantRepository = applicantRepository;
            _fileService = fileService;
        }
        public async Task<string> Handle(GetApplicantAttachmentsQuery query, CancellationToken cancellationToken)
        {
            string? response = null;
            if (query.ApplicantId == null)
            {
                throw new BadRequestException("The applicant id should not be null");
            }

            if (string.IsNullOrEmpty(query.AttachmentType))
            {
                throw new BadRequestException("The attachment type should not be null or empty.");
            }
            if (string.IsNullOrWhiteSpace(query.AttachmentType))
            {
                throw new BadRequestException("The attachment type should not be null or white space.");
            }
            response = Convert.ToBase64String(_fileService.getFile(query.ApplicantId.ToString(), query.AttachmentType, null).file);

            return response;
        }

    }
}