
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence
{
    public interface IOnlineApplicantRepository : IBaseRepository<OnlineApplicant>
    {
        Task<OnlineApplicant> GetByIdAsync(Guid id);
        public Task<bool> InsertOnlineApplicant(OnlineApplicant onlineApplicant, CancellationToken cancellationToken, string passportNumber);
        //Task<Int32> UpdateAsync(Partner partner);
    }
}