using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class GroupRepository : BaseRepository<UserGroup>, IGroupRepository
    {
        private readonly SmartAgencyDbContext _context;

        public GroupRepository(SmartAgencyDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        // async Task<UserGroup> IGroupRepository.GetByIdAsync(Guid id)
        // {
        //     return await base.GetAsync(id);
        // }
        public async Task<ICollection<UserGroup>> GetMultipleUserGroups(ICollection<Guid> groupIds)
        {
            return await _context.UserGroups.Where(ug => groupIds.Contains(ug.Id)).ToListAsync();
        }

        public async Task<List<UserGroup>> GetUserGroupByUserId(string userId)
        {
            var response = await _context.UserGroups.Where(ug => ug.ApplicationUsers.Any(apU => apU.Id == userId)).ToListAsync();
            return response;
        }
    }
}