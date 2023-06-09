using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Utility.Contracts;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence
{
    public interface IPageRepository: IBaseRepository<Page>
    {
        //Task GetAllWithSearchAsync(int pageNumber, int pageSize, string searchTerm, string orderBy, SortingDirection sortingDirection, string v);
        Task<Page> GetByIdAsync(Guid id);
        Task<Int32> UpdateAsync(Page page);
      
    }
}