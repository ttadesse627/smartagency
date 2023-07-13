
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Utility.Exceptions;
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.GetOrderDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities.Orders;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Orders.Query;
public record GetSingleOrder(Guid Id) : IRequest<GetOrderRespDTO>
{ }

public class GetSingleOrderHandler : IRequestHandler<GetSingleOrder, GetOrderRespDTO>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IFileService _fileService;

    public GetSingleOrderHandler(IOrderRepository orderRepository, IFileService fileService)
    {
        _orderRepository = orderRepository;
        _fileService = fileService;
    }
    public async Task<GetOrderRespDTO> Handle(GetSingleOrder request, CancellationToken cancellationToken)
    {
        // var orderResponse = new Order();
        var orderResponseDTO = new GetOrderRespDTO();
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
        var orderResponse = await _orderRepository.GetWithPredicateAsync(order => order.Id == request.Id && order.IsDeleted == false, eagerLoadedProperties);

        if (orderResponse != null)
        {
            // orderResponse.Success = true;
            orderResponseDTO = CustomMapper.Mapper.Map<GetOrderRespDTO>(orderResponse);

            // Set order attachment
            if (orderResponse.Attachment != null)
            {
                var folderName = orderResponse.Attachment.Title;
                if (folderName != null)
                {
                    var fileName = orderResponse.Id.ToString();
                    var orderFile = Convert.ToBase64String(_fileService.getFile(fileName, folderName).Item1);
                    var attachmentFile = new AttachmentFileResponseDTO
                    {
                        AttachmentFile = orderFile,
                        Attachment = new AttachmentDropDownDto
                        {
                            Key = orderResponse.Attachment.Id,
                            Value = orderResponse.Attachment.Title
                        }
                    };
                    orderResponseDTO.Attachment = attachmentFile;
                }

            }

            // Set sponsor attachment
            if (orderResponse.Sponsor.Attachment != null)
            {
                var folderName = orderResponse.Sponsor.Attachment.Title;
                if (folderName != null)
                {
                    var fileName = orderResponse.Sponsor.Id.ToString();
                    var sponsorFile = Convert.ToBase64String(_fileService.getFile(fileName, folderName).Item1);
                    var attachmentFile = new AttachmentFileResponseDTO
                    {
                        AttachmentFile = sponsorFile,
                        Attachment = new AttachmentDropDownDto
                        {
                            Key = orderResponse.Attachment.Id,
                            Value = orderResponse.Attachment.Title
                        }
                    };
                    orderResponseDTO.Sponsor.Attachment = attachmentFile;
                }
            }
        }
        else
        {
            throw new NotFoundException("You are trying to access the order that does not exist!");
        }
        return orderResponseDTO;
    }
}