

using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence;
public interface IApplicantProcessRepository : IBaseRepository<ApplicantProcess>
{
    
    public Task<object> GetDashbourdResult(DateTime? startDate, DateTime? endDate);
}