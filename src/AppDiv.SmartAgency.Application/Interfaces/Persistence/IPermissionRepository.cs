using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence
{
    public interface IPermissionRepository
    {
        Task<HashSet<Permission>> GetPermissionAsync(string userId);
    }
}