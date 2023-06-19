

using AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Attachments.Query
{
    public record GetDropDownAttachmentsQuery : IRequest<AttachmntResponseDTO> { }

    public class GetDropDownAttachmentsQueryHandler : IRequestHandler<GetDropDownAttachmentsQuery, AttachmntResponseDTO>
    {
        private readonly IAttachmentRepository _attachmentRepository;

        public GetDropDownAttachmentsQueryHandler(IAttachmentRepository attachmentRepository)
        {
            _attachmentRepository = attachmentRepository;
        }
        public async Task<AttachmntResponseDTO> Handle(GetDropDownAttachmentsQuery request, CancellationToken cancellationToken)
        {
            var attchdropDowns = await _attachmentRepository.GetAllWithAsync();
            var attachment = new AttachmntResponseDTO();
            var dropDownResponse = new List<AttachmentDropDownDto>();
            foreach (var attItem in attchdropDowns)
            {
                dropDownResponse.Add(new AttachmentDropDownDto { Key = attItem.Id, Value = attItem.Title });
            }

            attachment.Attachments = dropDownResponse;
            return attachment;

        }
    }
}