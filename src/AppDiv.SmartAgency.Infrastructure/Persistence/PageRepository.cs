using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;
using AppDiv.SmartAgency.Utility.Contracts;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class PageRepository: BaseRepository<Page>, IPageRepository
{
    private readonly SmartAgencyDbContext _context;
    public PageRepository(SmartAgencyDbContext dbContext) : base(dbContext)
    { 
            _context=dbContext;
    }

    public override async Task InsertAsync(Page page, CancellationToken cancellationToken)
    {
        await base.InsertAsync(page, cancellationToken);
    }
    public async Task<Page> GetByIdAsync(Guid Id)
    {
        return await base.GetAsync(Id);
    }

       /* public async Task GetAllWithSearchAsync(int pageNumber, int pageSize, string searchTerm, string orderBy, SortingDirection sortingDirection, string v)
        {
           await base.GetAllWithSearchAsync(pageNumber, pageSize,searchTerm, orderBy,sortingDirection);
        }
*/
         public async Task<Int32> UpdateAsync(Page page)
         {

              _context.Pages.Update(page);
              var response = await _context.SaveChangesAsync();

              return response;
          }
     
      }

    }
