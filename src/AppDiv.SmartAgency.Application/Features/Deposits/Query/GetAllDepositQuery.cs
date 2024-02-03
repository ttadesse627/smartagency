using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.DepositDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Deposits.Query
{
    public class GetAllDepositQuery : IRequest<SearchModel<DepositResponseDTO>>
    {

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SearchTerm { get; set; } = string.Empty;
        public string? OrderBy { get; set; } = string.Empty;
        public SortingDirection SortingDirection { get; set; } = SortingDirection.Ascending;
        public GetAllDepositQuery(int pageNumber, int pageSize, string? searchTerm, string? orderBy, SortingDirection sortingDirection)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchTerm = searchTerm;
            OrderBy = orderBy;
            SortingDirection = sortingDirection;
        }

    }

    public class GetAllDepositHandler : IRequestHandler<GetAllDepositQuery, SearchModel<DepositResponseDTO>>
    {
        private readonly IDepositRepository _depositRepository;
        private readonly ISmartAgencyDbContext _dbContext;

        public GetAllDepositHandler(IDepositRepository depositQueryRepository, ISmartAgencyDbContext dbContext)
        {
            _depositRepository = depositQueryRepository;
            _dbContext = dbContext;
        }
        public async Task<SearchModel<DepositResponseDTO>> Handle(GetAllDepositQuery request, CancellationToken cancellationToken)
        {
            var depositList = await _depositRepository.GetAllWithSearchAsync(request.PageNumber, request.PageSize, request.SearchTerm, request.OrderBy, request.SortingDirection, null, "Applicant");
            var depositResponse = CustomMapper.Mapper.Map<SearchModel<DepositResponseDTO>>(depositList);
            return depositResponse;

            // return (List<Customer>)await _customerQueryRepository.GetAllAsync();
        }
    }
}