

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.EnjazDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Deposits.Query
{
    public class GetAllEnjazsQuery : IRequest<SearchModel<EnjazResponseDTO>>
    {

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchTerm { get; set; } = string.Empty;
        public string OrderBy { get; set; } = string.Empty;
        public SortingDirection SortingDirection { get; set; } = SortingDirection.Ascending;
        public GetAllEnjazsQuery(int pageNumber, int pageSize, string searchTerm, string orderBy, SortingDirection sortingDirection)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchTerm = searchTerm;
            OrderBy = orderBy;
            SortingDirection = sortingDirection;
        }

    }

    public class GetAllEnjazsQueryHandler : IRequestHandler<GetAllEnjazsQuery, SearchModel<EnjazResponseDTO>>
    {
        private readonly IEnjazRepository _enjazRepository;
        private readonly ISmartAgencyDbContext _dbContext;

        public GetAllEnjazsQueryHandler(IEnjazRepository enjazRepository, ISmartAgencyDbContext dbContext)
        {
            _enjazRepository = enjazRepository;
            _dbContext = dbContext;
        }
        public async Task<SearchModel<EnjazResponseDTO>> Handle(GetAllEnjazsQuery request, CancellationToken cancellationToken)
        {
            var enjazsList = await _enjazRepository.GetAllWithSearchAsync(request.PageNumber, request.PageSize, request.SearchTerm, request.OrderBy, request.SortingDirection, null, "Order", "Order.Employees");
            var enjazResponse = new SearchModel<EnjazResponseDTO>();
            if (enjazsList.Items.Count() > 0 || enjazsList != null)
            {
                foreach (var enjaz in enjazsList.Items)
                {

                    if (enjaz.Order.Employees != null && enjaz.Order.Employees.Count > 0)
                    {
                        foreach (var employee in enjaz.Order.Employees)
                        {
                            var enjazResp = new EnjazResponseDTO
                            {
                                EnjazNumber = enjaz.ApplicationNumber,
                                OrderNumber = enjaz.Order.OrderNumber,
                                VisaNumber = enjaz.Order.VisaNumber,
                                OrderId = enjaz.OrderId,
                                PassportNumber = employee.PassportNumber,
                                FirstName = employee.FirstName,
                                MiddleName = employee.MiddleName,
                                LastName = employee.LastName
                            };
                            enjazResponse.Items.ToList().Add(enjazResp);
                        }
                    }

                }
            }
            var res=enjazResponse;
            return enjazResponse;

            // return (List<Customer>)await _customerQueryRepository.GetAllAsync();
        }
    }
}