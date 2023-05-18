

// using AppDiv.SmartAgency.Application.Features.Command.Create.Customers;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Attachments.Command.Create;
public class CreateAttachmentCommandHandler : IRequestHandler<CreateAttachmentCommand, CreateAttachmentCommandResponse>
{
        private readonly IAttachmentRepository _attachmentRepository;
        public CreateAttachmentCommandHandler(IAttachmentRepository attachmentRepository)
        {
            _attachmentRepository = attachmentRepository;
        }
        public async Task<CreateAttachmentCommandResponse> Handle(CreateAttachmentCommand request, CancellationToken cancellationToken)
        {
           // var customerEntity = CustomerMapper.Mapper.Map<Customer>(request.customer);           

            var createAttachmentCommandResponse = new CreateAttachmentCommandResponse();

            var validator = new CreateAttachmentCommandValidator(_attachmentRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            //Check and log validation errors
            if (validationResult.Errors.Count > 0)
            {
                createAttachmentCommandResponse.Success = false;
                createAttachmentCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                    createAttachmentCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                createAttachmentCommandResponse.Message = createAttachmentCommandResponse.ValidationErrors[0];
            }
            if (createAttachmentCommandResponse.Success)
            {
                //can use this instead of automapper
                var attachment = new Attachment()
                {
                    Code=request.attachment.Code,
                    Description=request.attachment.Description,
                    Category=request.attachment.Category,
                    IsRequired=request.attachment.IsRequired,
                    ShowOnCv=request.attachment.ShowOnCv
                };
                //
                await _attachmentRepository.InsertAsync(attachment, cancellationToken);
                await _attachmentRepository.SaveChangesAsync(cancellationToken);

                //var customerResponse = CustomerMapper.Mapper.Map<CustomerResponseDTO>(customer);
               // createCustomerCommandResponse.Customer = customerResponse;          
            }
            return createAttachmentCommandResponse;
        }
}