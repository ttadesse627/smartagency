

using AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Enums;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Command.Update.Attachments;

public class EditAttachmentCommand : IRequest<AttachmentResponseDTO>
{

    public string Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public AttachmentCategory Category { get; set; }
    public bool IsRequired { get; set; }
    public bool ShowOnCv { get; set; }
}

public class EditAttachmentCommandHandler : IRequestHandler<EditAttachmentCommand, AttachmentResponseDTO>
{
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly IAttachmentRepository _attachmentQueryRepository;
    public EditAttachmentCommandHandler(IAttachmentRepository attachmentRepository)
    {
        _attachmentRepository = attachmentRepository;
    }
    public async Task<AttachmentResponseDTO> Handle(EditAttachmentCommand request, CancellationToken cancellationToken)
    {
        // var attachmentEntity = CustomMapper.Mapper.Map<Attachment>(request);
        // Attachment attachEntity = new Attachment
        // {
        //     Id = request.Id,
        //     Code = request.Code,
        //     Description = request.Description,
        //     Category = request.Category,
        //     IsRequired = request.IsRequired,
        //     ShowOnCv = request.ShowOnCv
        // };

        try
        {
            await _attachmentRepository.UpdateAttachment(request);
        }
        catch (Exception exp)
        {
            throw new ApplicationException(exp.Message);
        }

        var modifiedAttachment = await _attachmentQueryRepository.GetByIdAsync(request.Id);
        var attachmentResponse = CustomMapper.Mapper.Map<AttachmentResponseDTO>(modifiedAttachment);

        return attachmentResponse;
    }
}
