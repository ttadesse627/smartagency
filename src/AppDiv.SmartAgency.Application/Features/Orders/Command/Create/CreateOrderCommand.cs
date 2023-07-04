using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Orders;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Orders;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Attachments.Command.Create;
public record CreateOrderCommand(CreateOrderRequest request) : IRequest<ServiceResponse<Int32>>
{ }
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ServiceResponse<Int32>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IApplicantRepository _applicantRepository;
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly IFileService _fileService;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IApplicantRepository applicantRepository, IAttachmentRepository attachmentRepository, IFileService fileService)
    {
        _orderRepository = orderRepository;
        _applicantRepository = applicantRepository;
        _attachmentRepository = attachmentRepository;
        _fileService = fileService;
    }
    public async Task<ServiceResponse<Int32>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var response = new ServiceResponse<Int32>();

        var orderEntity = CustomMapper.Mapper.Map<Order>(command.request);
        var orderPayment = new Payment { TotalAmount = command.request.Payment!.TotalAmount };
        orderPayment.AddPayment(command.request.Payment!.CurrentPaidAmount);
        orderEntity.Payment = orderPayment;
        IEnumerable<Applicant> appOrders = new List<Applicant>();
        var atchmnt = await _attachmentRepository.GetAsync(command.request.AttachmentFile!.AttachmentId);
        var spAtt = await _attachmentRepository.GetAsync(command.request.Sponsor!.AttachmentFile!.AttachmentId);
        if (command.request.EmployeeIds != null && command.request.EmployeeIds.Count > 0)
        {
            appOrders = await _applicantRepository.GetByIdsAsync(command.request.EmployeeIds, appl => appl.IsDeleted == false);
        }
        if (atchmnt != null)
        {
            orderEntity.Attachment = atchmnt;
        }
        if (spAtt != null)
        {
            orderEntity.Sponsor!.Attachment = spAtt;
        }
        if (appOrders.Count() > 0)
        {
            orderEntity.Employees = appOrders.ToList();
        }

        try
        {
            await _orderRepository.InsertAsync(orderEntity, cancellationToken);
            response.Success = await _orderRepository.SaveChangesAsync(cancellationToken);
        }
        catch (System.Exception ex)
        {
            // TODO
        }
        if (response.Success == true)
        {
            var orderFileSaved = false;
            var sponsorFileSaved = false;

            // save order attachment
            var orderFile = command.request.AttachmentFile?.AttachmentFile;
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

            // save sponsor attachment
            var sponsorFile = command.request.Sponsor?.AttachmentFile?.AttachmentFile;
            var sponsorAttachmentName = orderEntity.Sponsor!.Attachment!.Title;
            var sponsorFolderName = Path.Combine("Resources", sponsorAttachmentName!);
            var sponsorPathToSave = Path.Combine(Directory.GetCurrentDirectory(), sponsorFolderName);
            var sponsoFileName = orderEntity.Sponsor?.Id.ToString();
            if (!string.IsNullOrEmpty(sponsorFile))
            {
                orderFileSaved = await _fileService.UploadBase64FileAsync(sponsorFile, sponsoFileName!, sponsorPathToSave, FileMode.Create);
                if (sponsorFileSaved == true)
                {
                    response.Errors?.Add("Couldn't save sponsor attachment.");
                }
            }
        }

        return response;
    }
}