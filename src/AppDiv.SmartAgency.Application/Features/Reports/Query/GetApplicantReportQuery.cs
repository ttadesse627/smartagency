
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs.GetSingleApplResponseDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ReportDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Reports.Query;

 public class GetApplicantReportQuery : IRequest<SearchModel<ApplicantReportResponseDTO>>
{
   public int PageNumber { get; set; }
   public   int PageSize { get; set; }
   public  string? SearchTerm { get; set; }

    public GetApplicantReportQuery(int pageNumber, int pageSize, string searchTerm)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        SearchTerm = searchTerm;

    }
}
public class GetApplicantReportHandler : IRequestHandler<GetApplicantReportQuery, SearchModel<ApplicantReportResponseDTO>>
{
    private readonly IApplicantRepository _applicantRepository;
    public GetApplicantReportHandler(IApplicantRepository applicantRepository)
    {
        _applicantRepository = applicantRepository;
    }
    public async Task<SearchModel<ApplicantReportResponseDTO>> Handle(GetApplicantReportQuery request, CancellationToken cancellationToken)
    {
        //var applicantResponse = new SearchModel<ApplicantReportResponseDTO>();
        var fetchedApplicant = await _applicantRepository.GetAllWithSearchAsync(request.PageNumber,request.PageSize,request.SearchTerm, null, SortingDirection.Ascending, null,"MaritalStatus", "Religion", "BrokerName");
        return  CustomMapper.Mapper.Map<SearchModel<ApplicantReportResponseDTO>>(fetchedApplicant);
 
    }

}

