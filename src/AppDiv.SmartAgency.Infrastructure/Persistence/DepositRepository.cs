
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class DepositRepository : BaseRepository<Deposit>, IDepositRepository
{
    private readonly SmartAgencyDbContext _context;
    public DepositRepository(SmartAgencyDbContext dbContext) : base(dbContext)
    { 
            _context=dbContext;
    }

    public override async Task InsertAsync(Deposit deposit, CancellationToken cancellationToken)
    {
        
        await base.InsertAsync(deposit, cancellationToken);
    }
    public async Task<Deposit> GetByIdAsync(Guid Id)
    {
        return await base.GetAsync(Id);
    }
    public async Task<Int32> UpdateAsync(Deposit deposit)
   {
      
        _context.Deposits.Update(deposit);
        var response = await _context.SaveChangesAsync();

        return response;
    }
   
    }
}










