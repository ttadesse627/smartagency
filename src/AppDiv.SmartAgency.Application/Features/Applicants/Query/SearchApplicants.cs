// using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
// using AppDiv.SmartAgency.Application.Interfaces.Persistence;
// using AppDiv.SmartAgency.Application.Mapper;
// using AppDiv.SmartAgency.Utility.Contracts;
// using MediatR;

// namespace AppDiv.SmartAgency.Application.Features.Query.Applicants;
// public class SearchApplicants : IRequest<List<ApplicantsResponseDTO>>
// {
//     public string? SearchTerm { get; set; }
//     public string? OrderBy { get; set; }
//     public int PageNumber { get; set; }
//     public int PageSize { get; set; }
//     public SortingDirection SortOrderAscending { get; set; }
//     public SearchApplicants(string searchTerm,string orderBy, int pageNumber, int pageSize, SortingDirection sortOrderAscending)
//     {
//         SearchTerm = searchTerm;
//         OrderBy = orderBy;
//         PageNumber = pageNumber;
//         PageSize = pageSize;
//         SortOrderAscending = sortOrderAscending;
//     }
// }
// public class SearchApplicantsHandler : IRequestHandler<SearchApplicants, List<ApplicantsResponseDTO>>
// {
//     private readonly IApplicantRepository _applicantRepository;

//     public SearchApplicantsHandler(IApplicantRepository applicantRepository)
//     {
//         _applicantRepository = applicantRepository;
//     }
//     public async Task<List<ApplicantsResponseDTO>> Handle(SearchApplicants request, CancellationToken cancellationToken)
//     {
//         var applicantListResponse = await _applicantRepository.GetPagedSearchResultAsync(predicate: x => x.FirstName == request.SearchTerm, orderBy: x => x.FirstName, request.PageNumber, request.PageSize!, request.SortOrderAscending);
//         var applicantResponse = CustomMapper.Mapper.Map<List<ApplicantsResponseDTO>>(applicantListResponse.Entities);
//         return applicantResponse;
//     }
// }