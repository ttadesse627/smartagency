using AppDiv.SmartAgency.Application.Contracts.DTOs.ReportDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Reports;
public class NotAssignedApplicantReportQuery : IRequest<ApplReportDTO>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public List<FilterPropsRequest>? Filters { get; set; }
    public NotAssignedApplicantReportQuery(int pageNumber, int pageSize, List<FilterPropsRequest> filters)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Filters = filters;
    }
}
public class NotAssignedApplicantReportQueryHandler : IRequestHandler<NotAssignedApplicantReportQuery, ApplReportDTO>
{
    private readonly IApplicantRepository _applicantRepository;
    // private readonly ISmartAgencyDbContext _dbContext;

    public NotAssignedApplicantReportQueryHandler(IApplicantRepository applicantRepository)
    {
        _applicantRepository = applicantRepository;
        // _dbContext = dbContext;
    }
    public async Task<ApplReportDTO> Handle(NotAssignedApplicantReportQuery request, CancellationToken cancellationToken)
    {

        var response = new ApplReportDTO();
        var applicantResponse = new SearchModel<ApplicantReportResponseDTO>();
        var expLoadedProps = new string[] { "MaritalStatus", "Religion", "BrokerName" };
        var filters = new List<Filter>();
        foreach (var filter in request.Filters)
        {
            filters.Add(new Filter
            {
                PropertyName = filter.PropertyName,
                MethodName = filter.MethodName,
                Value = filter.Value
            }
        );
        }

        var applicantList = await _applicantRepository.GetAllWithPredicateFilterAsync(
            request.PageNumber, request.PageSize, filters, applicant => applicant.Order == null, expLoadedProps);

        applicantResponse = CustomMapper.Mapper.Map<SearchModel<ApplicantReportResponseDTO>>(applicantList);
        var itemsArray = applicantResponse.Items.ToArray();
        var entitiesArray = applicantList.Items.ToArray();
        for (var i = 0; i < itemsArray.Length; i++)
        {
            for (var j = 0; j < entitiesArray.Length; j++)
            {
                if (i == j)
                {
                    itemsArray[i].FullName = entitiesArray[j].FirstName + " " + entitiesArray[j].MiddleName + " " + entitiesArray[j].LastName;
                    itemsArray[i].RegistrationDate = entitiesArray[j].CreatedAt;
                    itemsArray[i].DeletedDate = entitiesArray[j].ModifiedAt;
                    itemsArray[i].PassportIssuedPlace = entitiesArray[j].PassportIssuedPlace?.Value;
                    itemsArray[i].MaritalStatus = entitiesArray[j].MaritalStatus?.Value;
                    itemsArray[i].Religion = entitiesArray[j].Religion?.Value;
                    itemsArray[i].Profession = entitiesArray[j].Jobtitle?.Value;
                    itemsArray[i].Experience = entitiesArray[j].Experience?.Value;
                    itemsArray[i].Language = entitiesArray[j].BrokerName?.Value;
                    itemsArray[i].BrokerName = entitiesArray[j].BrokerName?.Value;
                }
                if (i < j) break;
            }
        }
        applicantResponse.Items = itemsArray.AsEnumerable();
        response.Applicants = applicantResponse;

        var properties = await _applicantRepository.GetProperties();
        var propertyNames = new List<string>();
        foreach (var prop in properties)
        {
            propertyNames.Add(prop.Name);
        }
        response.FilterProperties = propertyNames;

        return response;
    }
}