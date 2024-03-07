
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Orders;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Orders.Command.Update;
public record EditOrderCommand(EditOrderRequest EditOrderRequest) : IRequest<ServiceResponse<Int32>>
{
}

public class EditOrderCommandHandler(IOrderRepository orderRepository, IApplicantRepository applicantRepository, IAttachmentRepository attachmentRepository, IFileService fileService) : IRequestHandler<EditOrderCommand, ServiceResponse<Int32>>
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IApplicantRepository _applicantRepository = applicantRepository;
    private readonly IAttachmentRepository _attachmentRepository = attachmentRepository;
    private readonly IFileService _fileService = fileService;

    public async Task<ServiceResponse<int>> Handle(EditOrderCommand request, CancellationToken cancellationToken)
    {
        var editOrderRequest = request.EditOrderRequest;
        var response = new ServiceResponse<int>();
        var updateResponse = new ServiceResponse<string>();

        var eagerLoadedProperties = new string[]
                                    {
                                        "PortOfArrival", "Priority","VisaType","PortOfArrival","Attachment","OrderCriteria","OrderCriteria.Nationality",
                                        "OrderCriteria.JobTitle","OrderCriteria.Salary","OrderCriteria.Religion","OrderCriteria.Experience",
                                        "OrderCriteria.Language","Sponsor","Sponsor.Address","Sponsor.Address.Region",
                                        "Sponsor.Address.Country","Sponsor.Address.City","Payment","Employees","Partner"
                                    };
        var serviceResponse = await _orderRepository.GetWithPredicateAsync(order => order.Id == editOrderRequest.Id, eagerLoadedProperties);

        var orderEntity = serviceResponse;
        if (orderEntity is not null)
        {
            if (!orderEntity.OrderCriteria!.Equals(null) || !orderEntity.Sponsor!.Equals(null) || !orderEntity.Payment!.Equals(null))
            {
                CustomMapper.Mapper.Map(request.EditOrderRequest, orderEntity);

            }
            if (editOrderRequest.EmployeeIds != null && editOrderRequest.EmployeeIds.Count > 0)
            {
                IEnumerable<Applicant> appOrders = new List<Applicant>();
                appOrders = await _applicantRepository.GetByIdsAsync(editOrderRequest.EmployeeIds, appl => appl.IsDeleted == false);

                if (appOrders.Any())
                {
                    orderEntity.Employees = appOrders.ToList();
                }
            }

            response.Success = await _orderRepository.SaveChangesAsync(cancellationToken);
            if (response.Success)
            {
                var orderFileSaved = false;
                var sponsorFileSaved = false;
                if (editOrderRequest.AttachmentFile != null)
                {
                    // save order attachment
                    var orderFile = editOrderRequest.AttachmentFile.AttachmentFile;
                    var orderAttachmentName = await _attachmentRepository.GetAsync((object)editOrderRequest.AttachmentFile?.AttachmentId!);
                    if (orderAttachmentName != null)
                    {
                        var orderFolderName = Path.Combine("Resources", orderAttachmentName.Title!);
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

                if (editOrderRequest.Sponsor != null && editOrderRequest.Sponsor?.AttachmentFile != null)
                {
                    // save sponsor attachment
                    var sponsorFile = editOrderRequest.Sponsor?.AttachmentFile?.AttachmentFile;
                    var sponsorAttachmentName = await _attachmentRepository.GetAsync((object)editOrderRequest.AttachmentFile?.AttachmentId!);
                    var sponsorFolderName = Path.Combine("Resources", sponsorAttachmentName.Title!);
                    var sponsorPathToSave = Path.Combine(Directory.GetCurrentDirectory(), sponsorFolderName);
                    var sponsoFileName = orderEntity.Sponsor!.Id.ToString();
                    if (!string.IsNullOrEmpty(sponsorFile))
                    {
                        orderFileSaved = await _fileService.UploadBase64FileAsync(sponsorFile, sponsoFileName, sponsorPathToSave, FileMode.Create);
                        if (sponsorFileSaved == true)
                        {
                            response.Errors?.Add("Couldn't save sponsor attachment.");
                        }
                    }
                }
                response.Message = $"Successfully updated the order with an id {editOrderRequest.Id}";
                response.Success = true;
            }
        }
        else if (orderEntity is null)
        {
            response.Message = $"An order with an Id {editOrderRequest.Id} is not found!";
            response.Success = false;
        }
        else throw new Exception("Unknown error occorred while trying to update the order.");
        return response;
    }
}