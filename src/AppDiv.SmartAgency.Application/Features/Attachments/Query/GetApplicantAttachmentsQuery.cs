using AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Attachments.Query
{
    public record GetApplicantAttachmentsQuery : IRequest<List<AttachmentDropDownDto>> { }
    public class GetApplicantAttachmentsQueryHandler : IRequestHandler<GetApplicantAttachmentsQuery, List<AttachmentDropDownDto>>
    {
        private readonly IAttachmentRepository _attachmentRepository;


        public GetApplicantAttachmentsQueryHandler(IAttachmentRepository attachmentRepository)
        {
            _attachmentRepository = attachmentRepository;

        }
        public async Task<List<AttachmentDropDownDto>> Handle(GetApplicantAttachmentsQuery request, CancellationToken cancellationToken)
        {

            return await _attachmentRepository.GetApplicantAttachments();
            // var attchdropDowns = await _attachmentRepository.GetAllWithAsync();
            // var attachment = new AttachmntResponseDTO();
            // var dropDownResponse = new List<AttachmentDropDownDto>();
            // foreach (var attItem in attchdropDowns)
            // {
            //     dropDownResponse.Add(new AttachmentDropDownDto { Key = attItem.Id, Value = attItem.Title });
            // }

            // attachment.Attachments = dropDownResponse;
            // return attachment;


        }
    }
}