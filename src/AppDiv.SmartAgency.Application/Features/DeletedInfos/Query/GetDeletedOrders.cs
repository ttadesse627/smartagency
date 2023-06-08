
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.GetOrdersDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.DeletedInfos.Query;

public class GetDeletedOrders : IRequest<SearchModel<GetOrdersResponseDTO>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string SearchTerm { get; set; } = string.Empty;
    public string OrderBy { get; set; } = string.Empty;
    public SortingDirection SortingDirection { get; set; } = SortingDirection.Ascending;
    public GetDeletedOrders(int pageNumber, int pageSize, string searchTerm, string orderBy, SortingDirection sortingDirection)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        SearchTerm = searchTerm;
        OrderBy = orderBy;
        SortingDirection = sortingDirection;
    }
}

public class GetDeletedOrdersHandler : IRequestHandler<GetDeletedOrders, SearchModel<GetOrdersResponseDTO>>
{
    private readonly IOrderRepository _orderRepository;

    public GetDeletedOrdersHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<SearchModel<GetOrdersResponseDTO>> Handle(GetDeletedOrders request, CancellationToken cancellationToken)
    {
        var eagerLoadedProperties = new string[]
                                    {
                                        "Priority","Sponsor", "OrderCriteria",
                                        "OrderCriteria.Salary",  "OrderCriteria.JobTitle",
                                        "Employee","Payment", "Employee", "Partner"
                                    };
        var orderList = await _orderRepository.GetAllWithPredicateSearchAsync
                        (
                            request.PageNumber, request.PageSize, request.SearchTerm, request.OrderBy,
                            request.SortingDirection, order => order.IsDeleted == true, eagerLoadedProperties
                        );
        var orderResponse = CustomMapper.Mapper.Map<SearchModel<GetOrdersResponseDTO>>(orderList);
        return orderResponse;
    }
}