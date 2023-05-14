
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Query.LookUps
{
    public record GetAllLookUps : IRequest<SearchModel<LookUpResponseDTO>>
    {

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchTerm { get; set; } = string.Empty;
        public string OrderBy { get; set; } = string.Empty;
        public SortingDirection SortingDirection { get; set; } = SortingDirection.Ascending;
        public GetAllLookUps(int pageNumber, int pageSize, string searchTerm, string orderBy, SortingDirection sortingDirection)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchTerm = searchTerm;
            OrderBy = orderBy;
            SortingDirection = sortingDirection;
        }

    }
}