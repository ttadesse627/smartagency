

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Attachments.Command.Delete;
public record DeleteAttachmentCommand(Guid Id) : IRequest<ServiceResponse<Int32>> { }
public class DeleteAttachmentCommandHandler : IRequestHandler<DeleteAttachmentCommand, ServiceResponse<Int32>>
{
    private readonly IAttachmentRepository _attachmentRepository;
    public DeleteAttachmentCommandHandler(IAttachmentRepository attachmentRepository)
    {
        _attachmentRepository = attachmentRepository;
    }
    public async Task<ServiceResponse<Int32>> Handle(DeleteAttachmentCommand request, CancellationToken cancellationToken)
    {
        var response = new ServiceResponse<Int32>();
        try
        {
            await _attachmentRepository.DeleteAsync(request.Id);
            response.Success = await _attachmentRepository.SaveChangesAsync(cancellationToken);
            if (response.Success)
            {
                response.Message = "Deletion Succeeded!";
            }
            else response.Errors.Add("Error occurred while saving the changes");
        }
        catch (Exception exp)
        {
            throw (new ApplicationException(exp.Message));
        }
        return response;
    }
}