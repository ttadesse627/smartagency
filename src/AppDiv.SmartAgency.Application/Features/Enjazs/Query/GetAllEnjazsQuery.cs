using AppDiv.SmartAgency.Application.Contracts.DTOs.EnjazDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Enjazs.Query
{
    public class GetAllEnjazsQuery : IRequest<SearchModel<EnjazResponseDTO>>
    {

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SearchTerm { get; set; } = string.Empty;
        public string? OrderBy { get; set; } = string.Empty;
        public SortingDirection SortingDirection { get; set; } = SortingDirection.Ascending;
        public GetAllEnjazsQuery(int pageNumber, int pageSize, string? searchTerm, string? orderBy, SortingDirection sortingDirection)
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
        private readonly IApplicantRepository _applicantRepository;
        private readonly ISmartAgencyDbContext _dbContext;

        public GetAllEnjazsQueryHandler(IApplicantRepository applicantRepository, ISmartAgencyDbContext dbContext)
        {
            _applicantRepository = applicantRepository;
            _dbContext = dbContext;
        }
        public async Task<SearchModel<EnjazResponseDTO>> Handle(GetAllEnjazsQuery request, CancellationToken cancellationToken)
        {
            var enjazsList = await _applicantRepository.GetAllWithSearchAsync(request.PageNumber, request.PageSize, request.SearchTerm, request.OrderBy, request.SortingDirection, applicant => applicant.Enjaz != null && applicant.OrderId != null, "Enjaz", "Order", "Order.Sponsor");
            var enjazResponse = new SearchModel<EnjazResponseDTO>();
            var enjazRespList = new List<EnjazResponseDTO>();
            if (enjazsList != null && enjazsList.Items.Any())
            {
                foreach (var applicant in enjazsList.Items)
                {
                    if (applicant.Enjaz != null)
                    {
                        var enjazResp = new EnjazResponseDTO
                        {
                            EnjazNumber = applicant.Enjaz.ApplicationNumber,
                            OrderNumber = applicant.Order!.OrderNumber,
                            VisaNumber = applicant.Order.VisaNumber,
                            EmployeeId = applicant.Id,
                            PassportNumber = applicant.PassportNumber,
                            EmployeeName = applicant.FirstName + " " + applicant.MiddleName + " " + applicant.LastName,
                            SponsorName = applicant.Order.Sponsor!.FullName
                        };
                        enjazRespList.Add(enjazResp);
                    }
                }
                enjazResponse.Items = enjazRespList;

            }
            return enjazResponse;
        }
    }
}