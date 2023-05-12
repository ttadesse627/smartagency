
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Query.LookUps
{
    public record GetAllLookUps : IRequest<PaginatedList<LookUpResponseDTO>>
    {
        public string SearchTerm { get; set; }
        public string[] ColumnNames { get; set; } = new string[]{"Value"};
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public GetAllLookUps(string searchTerm = "", int pageNumber = 1, int pageSize = 10)
        {
            SearchTerm = searchTerm;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

    }
}