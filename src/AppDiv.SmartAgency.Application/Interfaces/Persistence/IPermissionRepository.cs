

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence
{
    public interface IPermissionRepository
    {
        Task<HashSet<string>> GetPermissionAsync(string userId);
    }
}