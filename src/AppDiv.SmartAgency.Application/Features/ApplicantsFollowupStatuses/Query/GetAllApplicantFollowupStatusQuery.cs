using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantFollowupStatusDTOs;

using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.ApplicantsFollowupStatuses.Query
{
    public class GetAllApplicantFollowupStatusQuery : IRequest<SearchModel<ApplicantFollowupStatusResponseDTO>>
    {

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SearchTerm { get; set; } = string.Empty;
        public string? OrderBy { get; set; } = string.Empty;
        public SortingDirection SortingDirection { get; set; } = SortingDirection.Ascending;
        public GetAllApplicantFollowupStatusQuery(int pageNumber, int pageSize, string? searchTerm, string? orderBy, SortingDirection sortingDirection)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchTerm = searchTerm;
            OrderBy = orderBy;
            SortingDirection = sortingDirection;
        }

    }

    public class GetAllApplicantFollowupStatusHandler : IRequestHandler<GetAllApplicantFollowupStatusQuery, SearchModel<ApplicantFollowupStatusResponseDTO>>
    {
        private readonly IApplicantFollowupStatusRepository _applicantFollowupStatusRepository;
        private readonly ISmartAgencyDbContext _dbContext;

        public GetAllApplicantFollowupStatusHandler(IApplicantFollowupStatusRepository applicantFollowupStatusRepository, ISmartAgencyDbContext dbContext)
        {
            _applicantFollowupStatusRepository = applicantFollowupStatusRepository;
            _dbContext = dbContext;
        }
        public async Task<SearchModel<ApplicantFollowupStatusResponseDTO>> Handle(GetAllApplicantFollowupStatusQuery request, CancellationToken cancellationToken)
        {
            var followupList = await _applicantFollowupStatusRepository.GetAllWithSearchAsync(request.SearchTerm!);
            var paginatedFollowup = await _applicantFollowupStatusRepository.PaginateItems(request.PageNumber, request.PageSize, request.SortingDirection, followupList, request.OrderBy);
            var followupResponse = CustomMapper.Mapper.Map<SearchModel<ApplicantFollowupStatusResponseDTO>>(paginatedFollowup);
            return followupResponse;
        }
    }
}