

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Enums;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Attachments.Command.Update;

public class EditAttachmentCommand : IRequest<ServiceResponse<Int32>>
{

    public Guid Id { get; set; }
    public string? Title { get; set; }
    public AttachmentType Type { get; set; }
    public bool Required { get; set; }
    public bool ShowOnCv { get; set; }
}

public class EditAttachmentCommandHandler : IRequestHandler<EditAttachmentCommand, ServiceResponse<Int32>>
{
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly IAttachmentRepository _attachmentQueryRepository;
    public EditAttachmentCommandHandler(IAttachmentRepository attachmentRepository, IAttachmentRepository attachmentQueryRepository)
    {
        _attachmentRepository = attachmentRepository;
        _attachmentQueryRepository = attachmentQueryRepository;
    }
    public async Task<ServiceResponse<Int32>> Handle(EditAttachmentCommand request, CancellationToken cancellationToken)
    {
        var response = new ServiceResponse<Int32>();
        var modifiedAttachment = await _attachmentQueryRepository.GetAsync(request.Id);
        if (modifiedAttachment != null)
        {
            modifiedAttachment.Title = request.Title;
            modifiedAttachment.Type = request.Type;
            modifiedAttachment.Required = request.Required;
            modifiedAttachment.ShowOnCv = request.ShowOnCv;
            try
            {
                response.Success = await _attachmentRepository.SaveChangesAsync(cancellationToken);
                if (response.Success)
                {
                    response.Message = "Update Succeeded!";
                }
            }
            catch (Exception exp)
            {
                throw new System.ApplicationException(exp.Message);
            }
        }
        else
        {
            throw new NotFoundException($"The attachment with id {request.Id} doesn't found!");
        }

        return response;
    }
}
