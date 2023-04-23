

using AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Query.Attachments;
public class GetAllAttachmentsHandler : IRequestHandler<GetAllAttachments, List<AttachmentResponseDTO>>
{
    private readonly IAttachmentRepository _attachmentRepository;

        public GetAllAttachmentsHandler(IAttachmentRepository attachmentRepository)
        {
            _attachmentRepository = attachmentRepository;
        }
        public async Task<List<AttachmentResponseDTO>> Handle(GetAllAttachments request, CancellationToken cancellationToken)
        {
            var attachmentList = await _attachmentRepository.GetAllAsync();
            var attachmentResponse = CustomMapper.Mapper.Map<List<AttachmentResponseDTO>>(attachmentList);
            return attachmentResponse;
        }
}