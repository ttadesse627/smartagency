
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.GetOrderDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities.Orders;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Orders.Query;
public record GetSingleOrder(Guid Id) : IRequest<ServiceResponse<GetOrderRespDTO>>
{ }

public class GetSingleOrderHandler : IRequestHandler<GetSingleOrder, ServiceResponse<GetOrderRespDTO>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IFileService _fileService;

    public GetSingleOrderHandler(IOrderRepository orderRepository, IFileService fileService)
    {
        _orderRepository = orderRepository;
        _fileService = fileService;
    }
    public async Task<ServiceResponse<GetOrderRespDTO>> Handle(GetSingleOrder request, CancellationToken cancellationToken)
    {
        var orderResponse = new ServiceResponse<Order>();
        var orderResponseDTO = new ServiceResponse<GetOrderRespDTO>();
        var eagerLoadedProperties = new string[]
                                    {
                                        "PortOfArrival", "Priority","VisaType","Attachment",
                                        "Employees","OrderCriteria","OrderCriteria.Nationality",
                                        "OrderCriteria.JobTitle","OrderCriteria.Salary","OrderCriteria.Religion",
                                        "OrderCriteria.Experience","OrderCriteria.Language",
                                        "Sponsor","Sponsor.Attachment","Sponsor.Address",
                                        "Sponsor.Address.Region","Sponsor.Address.Country",
                                        "Sponsor.Address.City","Payment","Partner"
                                    };
        orderResponse.Data = await _orderRepository.GetWithPredicateAsync(order => order.Id == request.Id && order.IsDeleted == false, eagerLoadedProperties);

        if (orderResponse.Data != null)
        {
            orderResponse.Success = true;
            orderResponseDTO = CustomMapper.Mapper.Map<ServiceResponse<GetOrderRespDTO>>(orderResponse);
            if (orderResponse.Data!.Attachment != null)
            {
                var folderName = orderResponse.Data!.Attachment.Title;
                if (folderName != null)
                {
                    var fileName = orderResponse.Data!.Id.ToString();
                    var orderFile = Convert.ToBase64String(_fileService.getFile(fileName, folderName).Item1);
                    orderResponseDTO.Data.Attachment.AttachmentFile = fileName;
                    orderResponseDTO.Data.Attachment.Attachment.Key = orderResponse.Data!.Attachment.Id;
                    orderResponseDTO.Data.Attachment.Attachment.Value = orderResponse.Data!.Attachment.Title;
                }

            }
        }
        else
        {
            orderResponseDTO.Message = orderResponse.Message;
        }
        return orderResponseDTO;
    }
}