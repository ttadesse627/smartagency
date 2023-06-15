

using System.Linq.Expressions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence;
public interface IProcessRepository : IBaseRepository<Process>
{
    public Task<Int32> CreateProcessAsync(Process applicant);
    public Task<Int32> GetMaximumStepAsync(Expression<Func<Process, bool>> predicate);
}