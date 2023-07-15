using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Orders;
using AppDiv.SmartAgency.Application.Features.Orders.Command.Update;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Orders;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Attachments.Command.Create;
public record CreateOrderCommand(CreateOrderRequest Request) : IRequest<ServiceResponse<Int32>> { }
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ServiceResponse<Int32>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IApplicantRepository _applicantRepository;
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly IFileService _fileService;
    private readonly IMediator _mediator;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IApplicantRepository applicantRepository, IAttachmentRepository attachmentRepository, IFileService fileService, IMediator mediator)
    {
        _orderRepository = orderRepository;
        _applicantRepository = applicantRepository;
        _attachmentRepository = attachmentRepository;
        _fileService = fileService;
        _mediator = mediator;
    }
    public async Task<ServiceResponse<Int32>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var response = new ServiceResponse<Int32>();

        var orderEntity = CustomMapper.Mapper.Map<Order>(command.Request);
        var orderPayment = new Payment { TotalAmount = command.Request.Payment!.TotalAmount };
        orderPayment.AddPayment(command.Request.Payment!.CurrentPaidAmount);
        orderEntity.Payment = orderPayment;

        try
        {
            await _orderRepository.InsertAsync(orderEntity, cancellationToken);
            response.Success = await _orderRepository.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Errors?.Add(ex.Message);
        }

        if (response.Success == true)
        {
            // save order attachment
            if (command.Request.AttachmentFile != null)
            {
                if (command.Request.AttachmentFile.AttachmentId != null && command.Request.AttachmentFile.AttachmentFile != null)
                {
                    var atchmnt = await _attachmentRepository.GetAsync(command.Request.AttachmentFile.AttachmentId);
                    if (atchmnt != null)
                    {
                        orderEntity.Attachment = atchmnt;
                        var orderFileSaved = false;
                        var orderFile = command.Request.AttachmentFile?.AttachmentFile;
                        var orderFolderName = Path.Combine("Resources", atchmnt!.Title!);
                        var orderPathToSave = Path.Combine(Directory.GetCurrentDirectory(), orderFolderName);
                        var orderFileName = orderEntity.Id.ToString();
                        if (!string.IsNullOrEmpty(orderFile))
                        {
                            orderFileSaved = await _fileService.UploadBase64FileAsync(orderFile, orderFileName, orderPathToSave, FileMode.Create);
                            if (orderFileSaved == true)
                            {
                                response.Errors?.Add("Couldn't save order attachment.");
                            }
                        }
                    }
                }
            }

            // save sponsor attachment
            if (command.Request.Sponsor.AttachmentFile != null)
            {
                if (command.Request.Sponsor.AttachmentFile.AttachmentId != null && command.Request.Sponsor.AttachmentFile.AttachmentFile != null)
                {
                    var atchmnt = await _attachmentRepository.GetAsync(command.Request.Sponsor.AttachmentFile.AttachmentId);
                    if (atchmnt != null)
                    {
                        orderEntity.Attachment = atchmnt;
                        var sponsorFileSaved = false;
                        var orderFile = command.Request.AttachmentFile?.AttachmentFile;
                        var orderFolderName = Path.Combine("Resources", atchmnt!.Title!);
                        var orderPathToSave = Path.Combine(Directory.GetCurrentDirectory(), orderFolderName);
                        var orderFileName = orderEntity.Id.ToString();
                        if (!string.IsNullOrEmpty(orderFile))
                        {
                            sponsorFileSaved = await _fileService.UploadBase64FileAsync(orderFile, orderFileName, orderPathToSave, FileMode.Create);
                            if (sponsorFileSaved)
                            {
                                response.Errors?.Add("Couldn't save sponsor attachment.");
                            }
                        }
                    }
                }
            }

            if (command.Request.EmployeeIds != null && command.Request.EmployeeIds.Any())
            {
                var assignment = new OrderAssignmentRequest();
                var orderAssigmentList = new List<OrderAssignment>();
                foreach (var employeeId in command.Request.EmployeeIds)
                {
                    var assignRequest = new OrderAssignment
                    {
                        EmployeeId = employeeId,
                        OrderId = orderEntity.Id
                    };
                    orderAssigmentList.Add(assignRequest);
                }
                assignment.OrderAssignments = orderAssigmentList;

                try
                {
                    var assignResponse = await _mediator.Send(new AssignOrderCommand(assignment));
                    if (!assignResponse.Success)
                    {
                        response.Errors?.Add("Could not assign the applicant to the order.");
                    }
                }
                catch (Exception ex)
                {
                    response.Errors?.Add(ex.Message);
                }
            }
        }

        return response;
    }
}
