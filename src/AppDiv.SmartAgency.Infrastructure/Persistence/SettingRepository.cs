using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class SettingRepository : BaseRepository<Setting>, ISettingRepository
    {
        // private readonly CRVSDbContext dbContext;
        // private readonly ISettingCouchRepository settingCouchRepo;
        // public SettingRepository(CRVSDbContext dbContext, ISettingCouchRepository settingCouchRepo) : base(dbContext)
        // {
        //     this.dbContext = dbContext;
        //     this.settingCouchRepo = settingCouchRepo;
        // }




        private readonly SmartAgencyDbContext _context;
        public SettingRepository(SmartAgencyDbContext context) : base(context)
        {
            _context = context;
        }


        async Task<Setting> ISettingRepository.GetSettingByKey(string key)
        {
            return await base.GetAsync(key);
        }
        async Task<Setting> ISettingRepository.GetByIdAsync(Guid id)
        {
            return await base.GetAsync(id);
        }

        /*  public virtual async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
          {

              var entries = _context.ChangeTracker
                .Entries()
                .Where(e => e.Entity is Setting &&
                        (e.State == EntityState.Added
                        || e.State == EntityState.Modified || e.State == EntityState.Deleted));
              List<SettingEntry> settingEntries = entries.Select(e => new SettingEntry
              {
                  State = e.State,
                  Setting = CustomMapper.Mapper.Map<SettingDTO>((Setting)e.Entity)
              }).ToList();

              bool saveRes = await base.SaveChangesAsync(cancellationToken);

              if (saveRes)
              {
                  foreach (var entry in settingEntries)
                  {
                      switch (entry.State)
                      {
                          case EntityState.Added:
                              await settingCouchRepo.InsertSettingAsync(entry.Setting);
                              break;
                          case EntityState.Modified:
                              await settingCouchRepo.UpdateSettingAsync(entry.Setting);
                              break;
                          case EntityState.Deleted:
                              await settingCouchRepo.RemoveSettingAsync(entry.Setting);
                              break;
                          default: break;

                      }
                  }

              }
              return saveRes;


          }
          public async Task InitializeSettingCouch()
          {
              var empty = await settingCouchRepo.IsEmpty();
              if (empty)
              {
                  await settingCouchRepo.BulkInsertAsync(dbContext.Settings);
              }
          }*/
    }
}