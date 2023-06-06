

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Attachments;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Attachments.Command.Create;
public record CreateAttachmentCommand(CreateAttachmentRequest attachment) : IRequest<ServiceResponse<Int32>> { }
public class CreateAttachmentCommandHandler : IRequestHandler<CreateAttachmentCommand, ServiceResponse<Int32>>
{
    private readonly IAttachmentRepository _attachmentRepository;
    public CreateAttachmentCommandHandler(IAttachmentRepository attachmentRepository)
    {
        _attachmentRepository = attachmentRepository;
    }
    public async Task<ServiceResponse<Int32>> Handle(CreateAttachmentCommand request, CancellationToken cancellationToken)
    {

        var response = new ServiceResponse<Int32>();

        var validator = new CreateAttachmentCommandValidator(_attachmentRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        //Check and log validation errors
        if (validationResult.Errors.Count > 0)
        {
            response.Success = false;
            response.Errors = new List<string>();
            foreach (var error in validationResult.Errors)
                response.Errors.Add(error.ErrorMessage);
            response.Message = response.Errors[0];
        }
        if (validationResult.IsValid)
        {
            //can use this instead of automapper
            var attachment = new Attachment()
            {
                Title = request.attachment.Title,
                Type = request.attachment.Type,
                Required = request.attachment.Required,
                ShowOnCv = request.attachment.ShowOnCv
            };
            //
            await _attachmentRepository.InsertAsync(attachment, cancellationToken);
            response.Success = await _attachmentRepository.SaveChangesAsync(cancellationToken);
            if (response.Success)
            {
                response.Message = "Successfully added!";
            }
            else throw new ValidationException();
        }
        return response;
    }
}