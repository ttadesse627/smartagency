using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.GetOrdersDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Orders.Query;

public record GetAllOrders : IRequest<SearchModel<GetOrdersResponseDTO>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string SearchTerm { get; set; } = string.Empty;
    public string OrderBy { get; set; } = string.Empty;
    public SortingDirection SortingDirection { get; set; } = SortingDirection.Ascending;
    public GetAllOrders(int pageNumber, int pageSize, string searchTerm, string orderBy, SortingDirection sortingDirection)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        SearchTerm = searchTerm;
        OrderBy = orderBy;
        SortingDirection = sortingDirection;
    }

}

public class GetAllOrdersHandler : IRequestHandler<GetAllOrders, SearchModel<GetOrdersResponseDTO>>
{

    private readonly IOrderRepository _orderRepository;

    public GetAllOrdersHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<SearchModel<GetOrdersResponseDTO>> Handle(GetAllOrders request, CancellationToken cancellationToken)
    {
        var orderResponse = new SearchModel<GetOrdersResponseDTO>();
        var orderDTOs = new List<GetOrdersResponseDTO>();
        var eagerLoadedProperties = new string[]
                                    {
                                        "Priority","Sponsor", "OrderCriteria",
                                        "OrderCriteria.Salary",  "OrderCriteria.JobTitle",
                                        "Employees","Payment", "Employees", "Partner"
                                    };
        var orderList = await _orderRepository.GetAllWithPredicateSearchAsync
                        (
                            request.PageNumber, request.PageSize, request.SearchTerm, request.OrderBy,
                            request.SortingDirection, order => order.IsDeleted == false, eagerLoadedProperties
                        );

        if (orderList.Items != null && orderList.Items.Any())
        {
            foreach (var order in orderList.Items)
            {
                if (order.Employees != null && order.Employees.Any())
                {
                    foreach (var employee in order.Employees)
                    {
                        var orderResp = new GetOrdersResponseDTO
                        {
                            Id = order.Id,
                            RegisteredDate = order.CreatedAt,
                            OrderNumber = order.OrderNumber,
                            VisaNumber = order.VisaNumber,
                            Priority = order.Priority?.Value,
                            ApplicantId = employee.Id,
                            PassportNumber = employee.PassportNumber,
                            EmployeeName = employee.FirstName + " " + employee.MiddleName + " " + employee.LastName,
                            JobTitle = order.OrderCriteria?.JobTitle?.Value,
                            Salary = order.OrderCriteria?.Salary?.Value,
                            TotalAmount = order.Payment.TotalAmount,
                            PaidAmount = order.Payment.PaidAmount,
                            SponsorIdNumber = order.Sponsor?.IdNumber,
                            SponsorFullName = order.Sponsor.FullName,
                            PartnerName = order.Partner.PartnerName
                        };
                        orderDTOs.Add(orderResp);
                        orderList.TotalCount += 1;
                    }
                }
                else
                {
                    var orderResp = new GetOrdersResponseDTO
                    {
                        Id = order.Id,
                        RegisteredDate = order.CreatedAt,
                        OrderNumber = order.OrderNumber,
                        VisaNumber = order.VisaNumber,
                        Priority = order.Priority?.Value,
                        ApplicantId = null,
                        PassportNumber = null,
                        EmployeeName = null,
                        JobTitle = order.OrderCriteria?.JobTitle?.Value,
                        Salary = order.OrderCriteria?.Salary?.Value,
                        TotalAmount = order.Payment.TotalAmount,
                        PaidAmount = order.Payment.PaidAmount,
                        SponsorIdNumber = order.Sponsor?.IdNumber,
                        SponsorFullName = order.Sponsor.FullName,
                        PartnerName = order.Partner.PartnerName
                    };
                    orderDTOs.Add(orderResp);
                }
            }
        }
        orderResponse.Items = orderDTOs;
        orderResponse.CurrentPage = orderList.CurrentPage;
        orderResponse.MaxPage = orderList.MaxPage;
        orderResponse.PagingSize = orderList.PagingSize;
        orderResponse.SearchKeyWord = orderList.SearchKeyWord;
        orderResponse.SortingColumn = orderList.SortingColumn;
        orderResponse.SortingDirection = orderList.SortingDirection;
        orderResponse.TotalCount = orderList.TotalCount;

        // var orderResponse = CustomMapper.Mapper.Map<SearchModel<GetOrdersResponseDTO>>(orderList);
        return orderResponse;
    }
}