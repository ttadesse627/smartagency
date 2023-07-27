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
    public class OnlineApplicantRepository : BaseRepository<OnlineApplicant>, IOnlineApplicantRepository
    {
        private readonly SmartAgencyDbContext _context;
        public OnlineApplicantRepository(SmartAgencyDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public override async Task InsertAsync(OnlineApplicant onlineApplicant, CancellationToken cancellationToken)
        {


            await base.InsertAsync(onlineApplicant, cancellationToken);
        }

        public async Task<OnlineApplicant> GetByIdAsync(Guid Id)
        {
            //return await base.GetAsync(Id);
            var onlineApplicant = _context.OnlineApplicants
                .Include(a => a.MaritalStatus)
                .Include(a => a.Experience)
                .Include(a => a.DesiredCountry)
                .FirstOrDefault(a => a.Id == Id);

            return onlineApplicant;

        }

        public async Task<int> GetOnlineApplicantByPassportNumber(string passportNumber)
        {

            var res = await _context.OnlineApplicants.CountAsync(oa => oa.Passport == passportNumber);
            return res;
        }

        public Task<bool> InsertOnlineApplicant(OnlineApplicant onlineApplicant, CancellationToken cancellationToken, string passportNumber)
        {
            throw new NotImplementedException();
        }
        // public async Task<string> InsertOnlineApplicant(OnlineApplicant onlineApplicant, CancellationToken cancellationToken, string passportNumber)
        // {
        //     var existedOnlineApplicant = await _context.OnlineApplicants.CountAsync(oApp => oApp.Passport == passportNumber);
        //     if (existedOnlineApplicant > 0)
        //     {
        //         return $"An Applicant with {passportNumber} Aleardy Existed ";
        //     }
        //     await base.InsertAsync(onlineApplicant, cancellationToken);
        //     if (await base.SaveChangesAsync(cancellationToken))
        //     {
        //         return "Your Application has been Submitted";
        //     }

        // }

    }
}