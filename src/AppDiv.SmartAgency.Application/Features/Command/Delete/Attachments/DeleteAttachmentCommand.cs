

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Command.Delete.Attachments;
public class DeleteAttachmentCommand : IRequest<ServiceResponse<List<AttachmentResponseDTO>>>
{
    public string Id { get; set; }
}
public class DeleteAttachmentCommandHandler : IRequestHandler<DeleteAttachmentCommand, ServiceResponse<List<AttachmentResponseDTO>>>
{
    private readonly IAttachmentRepository _attachmentRepository;
    public DeleteAttachmentCommandHandler(IAttachmentRepository attachmentRepository)
    {
        _attachmentRepository = attachmentRepository;
    }
    public async Task<ServiceResponse<List<AttachmentResponseDTO>>> Handle(DeleteAttachmentCommand request, CancellationToken cancellationToken)
    {
        var response = new ServiceResponse<List<AttachmentResponseDTO>>();
        try
        {
            Console.WriteLine("First Call From handler");
            response = await _attachmentRepository.DeleteAttachment(request.Id);
            if (response is not null) Console.WriteLine("Try Success!");
        }
        catch (Exception exp)
        {
            throw (new ApplicationException(exp.Message));
        }
        return response;
    }
}