using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class PageRepository : BaseRepository<Page>, IPageRepository
    {
        private readonly SmartAgencyDbContext _context;
        public PageRepository(SmartAgencyDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public override async Task InsertAsync(Page page, CancellationToken cancellationToken)
        {
            await base.InsertAsync(page, cancellationToken);
        }
        public async Task<Page?> GetByIdAsync(Guid Id)
        {
            return await base.GetAsync(Id);
        }

        public async Task<Int32> UpdateAsync(Page page)
        {

            _context.Pages.Update(page);
            var response = await _context.SaveChangesAsync();

            return response;
        }

    }

}
