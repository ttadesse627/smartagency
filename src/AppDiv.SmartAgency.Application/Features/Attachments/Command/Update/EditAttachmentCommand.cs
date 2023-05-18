

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Enums;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Attachments.Command.Update;

public class EditAttachmentCommand : IRequest<ServiceResponse<AttachmentResponseDTO>>
{

    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public AttachmentCategory Category { get; set; }
    public bool IsRequired { get; set; }
    public bool ShowOnCv { get; set; }
}

public class EditAttachmentCommandHandler : IRequestHandler<EditAttachmentCommand, ServiceResponse<AttachmentResponseDTO>>
{
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly IAttachmentRepository _attachmentQueryRepository;
    public EditAttachmentCommandHandler(IAttachmentRepository attachmentRepository, IAttachmentRepository attachmentQueryRepository)
    {
        _attachmentRepository = attachmentRepository;
        _attachmentQueryRepository = attachmentQueryRepository;
    }
    public async Task<ServiceResponse<AttachmentResponseDTO>> Handle(EditAttachmentCommand request, CancellationToken cancellationToken)
    {
        var response = new ServiceResponse<AttachmentResponseDTO>();

        try
        {
            response = await _attachmentRepository.UpdateAttachment(request);
        }
        catch (Exception exp)
        {
            throw new ApplicationException(exp.Message);
        }

        var modifiedAttachment = await _attachmentQueryRepository.GetByIdAsync(request.Id);
        var attachmentResponse = CustomMapper.Mapper.Map<AttachmentResponseDTO>(modifiedAttachment);

        return response;
    }
}
