using System.Linq.Expressions;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Query;
public record GetApplSearchResultQuery : IRequest<SearchModel<ApplSearchResponseDTO>>
{
    public Guid? JobtitleId { get; set; }
    public Guid? MaritalStatusId { get; set; }
    public int AgeFrom { get; set; }
    public int AgeTo { get; set; }
    public Guid? ReligionId { get; set; }
    public Guid? ExperienceId { get; set; }
    public Guid? CountryId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? OrderBy { get; set; }
    public SortingDirection SortingDirection { get; set; }

    public GetApplSearchResultQuery
    (
        int pageNumber, int pageSize, string? orderBy, SortingDirection sortingDirection,
        Guid? jobtitleId, Guid? maritalStatusId, int ageFrom, int ageTo,
        Guid? religionId, Guid? experienceId, Guid? countryId
    )
    {
        JobtitleId = jobtitleId;
        MaritalStatusId = maritalStatusId;
        AgeFrom = ageFrom;
        AgeTo = ageTo;
        ReligionId = religionId;
        ExperienceId = experienceId;
        CountryId = countryId;
        PageNumber = pageNumber;
        PageSize = pageSize;
        OrderBy = orderBy;
        SortingDirection = sortingDirection;
    }
}
public class GetApplSearchResultQueryHandler(IApplicantRepository applicantRepository, IFileService fileService) : IRequestHandler<GetApplSearchResultQuery, SearchModel<ApplSearchResponseDTO>>
{
    private readonly IApplicantRepository _applicantRepository = applicantRepository;
    private readonly IFileService _fileService = fileService;

    public async Task<SearchModel<ApplSearchResponseDTO>> Handle(GetApplSearchResultQuery request, CancellationToken cancellationToken)
    {
        var eagerLoadedProperties = new string[] { "Jobtitle", "MaritalStatus", "Religion", "Experience", "IssuingCountry", "DesiredCountry", "RequestedApplicant" };
        var expressions = new List<Expression<Func<Applicant, bool>>>();
        var searchResponse = new SearchModel<ApplSearchResponseDTO>();

        if (request != null)
        {
            if (request.JobtitleId != null)
            {
                expressions.Add(app => app.JobtitleId == request.JobtitleId);
            }
            if (request.MaritalStatusId != null)
            {
                expressions.Add(app => app.MaritalStatusId == request.MaritalStatusId);
            }
            if (request.ReligionId != null)
            {
                expressions.Add(app => app.ReligionId == request.ReligionId);
            }
            if (request.ExperienceId != null)
            {
                expressions.Add(app => app.ExperienceId == request.ExperienceId);
            }
            if (request.CountryId != null)
            {
                expressions.Add(app => app.IssuingCountryId == request.CountryId);
            }
            if (request.AgeFrom != 0 || request.AgeTo != 0)
            {
                var maxBirthDate = DateTime.Today.AddYears(-request.AgeFrom);
                var minBirthDate = DateTime.Today.AddYears(-request.AgeTo);
                expressions.Add(app => app.BirthDate >= maxBirthDate && app.BirthDate >= minBirthDate);
            }

            expressions.Add(app => app.IsDeleted == false && app.RequestedApplicant == null && app.OrderId == null);
        }
        var searchResult = await _applicantRepository.GetAllApplWithPredicateAsync
                                    (
                                        request!.PageNumber, request.PageSize, request.OrderBy, request.SortingDirection,
                                        expressions, eagerLoadedProperties
                                    );

        searchResponse = CustomMapper.Mapper.Map<SearchModel<ApplSearchResponseDTO>>(searchResult);

        if (searchResponse != null || searchResponse?.Items.Count() > 0)
        {
            foreach (var item in searchResponse.Items)
            {
                item.Path = "api/applicant/get-cv-detail/" + item.Id;
                item.FullName = searchResult.Items.FirstOrDefault(sr => sr.Id == item.Id)?.AmharicFullName;
                item.Photo = Convert.ToBase64String(_fileService.getFile(item.Id.ToString(), "Full Size", null).Item1);
            }
        }
        return searchResponse!;
    }
}