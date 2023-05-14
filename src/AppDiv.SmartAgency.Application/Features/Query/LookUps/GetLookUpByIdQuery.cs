using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Query.LookUps
{
    public class GetLookUpByIdQuery: IRequest<LookUp>
    {
        public Guid Id { get; private set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchTerm { get; set; } = string.Empty;
        public string SearchByColumnName { get; set; } = string.Empty;
        public string OrderBy { get; set; } = string.Empty;
        public SortingDirection SortingDirection { get; set; } = SortingDirection.Ascending;

        public GetLookUpByIdQuery(Guid Id, int pageNumber, int pageSize, string searchTerm, string searchByColumnName, string orderBy, SortingDirection sortingDirection)
        {
            this.Id = Id;
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchTerm = searchTerm;
            SearchByColumnName = searchByColumnName;
            OrderBy = orderBy;
            SortingDirection = sortingDirection;
        }
    }
}