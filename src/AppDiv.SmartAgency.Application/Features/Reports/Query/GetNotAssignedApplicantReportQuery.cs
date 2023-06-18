using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ReportDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Reports.Query
{
    public class GetNotAssignedApplicantReportQuery : IRequest<SearchModel<ApplicantReportResponseDTO>>
    {
       public int PageNumber { get; set; }
       public int PageSize { get; set; }
       public string? SearchTerm { get; set; }
       public GetNotAssignedApplicantReportQuery(int pageNumber, int pageSize, string searchTerm)
       {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchTerm = searchTerm;
       }
    }
    public class GetNotAssignedApplicantReportHandler :IRequestHandler<GetNotAssignedApplicantReportQuery, SearchModel<ApplicantReportResponseDTO>>
    {
        private readonly IApplicantRepository _applicantRepository;
        public GetNotAssignedApplicantReportHandler(IApplicantRepository applicantRepository)
        {
            _applicantRepository = applicantRepository;
        }

          public async Task<SearchModel<ApplicantReportResponseDTO>> Handle(GetNotAssignedApplicantReportQuery request, CancellationToken cancellationToken)
          {
            var fetchedApplicant = await _applicantRepository.GetAllWithPredicateSearchAsync(request.PageNumber, request.PageSize, request.SearchTerm, null, SortingDirection.Ascending, applicant => applicant.Order == null, "MaritalStatus", "Religion", "BrokerName");
            return CustomMapper.Mapper.Map<SearchModel<ApplicantReportResponseDTO>>(fetchedApplicant);
        }
    }
}