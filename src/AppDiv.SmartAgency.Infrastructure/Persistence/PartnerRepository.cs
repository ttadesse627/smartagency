using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class PartnerRepository : BaseRepository<Partner>, IPartnerRepository
{
    private readonly SmartAgencyDbContext _context;
    public PartnerRepository(SmartAgencyDbContext dbContext) : base(dbContext)
    { 
            _context=dbContext;
    }

    public override async Task InsertAsync(Partner partner, CancellationToken cancellationToken)
    {
        await base.InsertAsync(partner, cancellationToken);
    }
    public async Task<Partner> GetByIdAsync(Guid Id)
        {

    var partner =  _context.Partners
        .Include(p => p.Address)
        .ThenInclude(c => c.Country)
        .FirstOrDefault(p => p.Id == Id);
       return  partner;
            //return await base.GetAsync(Id);
         /*  var onlineApplicant=   _context.OnlineApplicants
               .Include(a => a.MaritalStatus)
               .Include(a => a.Experience)
               .Include(a => a.DesiredCountry)
               .FirstOrDefault(a => a.Id == Id);
                  
            return onlineApplicant;
            */
        }
    
   public async Task<Int32> UpdateAsync(Partner partner)
   {
      
        _context.Partners.Update(partner);
        var response = await _context.SaveChangesAsync();

        return response;
    }
}
}