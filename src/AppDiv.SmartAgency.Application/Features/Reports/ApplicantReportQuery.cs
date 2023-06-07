using AppDiv.SmartAgency.Application.Contracts.DTOs.ReportDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Reports;
public class ApplicantReportQuery : IRequest<(SearchModel<ApplicantReportResponseDTO>, List<string>)>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SearchTerm { get; set; }
    public string? OrderBy { get; set; }
    public SortingDirection SortingDirection { get; set; } = SortingDirection.Ascending;
    public List<(string propertyName, string methodName, Object value)>? Filters { get; set; }
    public ApplicantReportQuery(int pageNumber, int pageSize, string? searchTerm, string? orderBy, SortingDirection sortingDirection,
        List<(string propertyName, string methodName, Object value)>? filters)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        SearchTerm = searchTerm;
        OrderBy = orderBy;
        SortingDirection = sortingDirection;
        Filters = filters;
    }
}
public class GetAllApplicantsHandler : IRequestHandler<ApplicantReportQuery, (SearchModel<ApplicantReportResponseDTO>, List<string>)>
{
    private readonly IApplicantRepository _applicantRepository;
    // private readonly ISmartAgencyDbContext _dbContext;

    public GetAllApplicantsHandler(IApplicantRepository applicantRepository)
    {
        _applicantRepository = applicantRepository;
        // _dbContext = dbContext;
    }
    public async Task<(SearchModel<ApplicantReportResponseDTO>, List<string>)> Handle(ApplicantReportQuery request, CancellationToken cancellationToken)
    {
        var expLoadedProps = new string[] { "MaritalStatus", "Religion", "BrokerName" };
        var filters = new List<Filter>();

        var properties = await _applicantRepository.GetProperties();
        var stringProperties = new List<string>();
        foreach (var prop in properties)
        {
            if (prop.PropertyType == typeof(string) || prop.PropertyType == typeof(int) || prop.PropertyType == typeof(float) || prop.PropertyType == typeof(DateTime))
            {
                stringProperties.Add(prop.ToString());
            }
        }
        var applicantList = await _applicantRepository.GetAllWithFilterAsync(
            request.PageNumber, request.PageSize, request.SearchTerm, request.OrderBy, request.SortingDirection,
            filters, expLoadedProps);
        var response = CustomMapper.Mapper.Map<SearchModel<ApplicantReportResponseDTO>>(applicantList);
        var itemsArray = response.Items.ToArray();
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
        response.Items = itemsArray.AsEnumerable();

        return (response, stringProperties);
    }
}