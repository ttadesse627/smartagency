

using System.Linq.Expressions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence;
public interface IProcessRepository : IBaseRepository<Process>
{
    public Task<Int32> GetMaximumStepAsync(Expression<Func<Process, bool>> predicate);
    public Task<bool> GetMinStepProcessesAsync(Guid? processId);
    public Task<IEnumerable<Process>> GetEnjazRequiredProcessesAsync();
    public Task<IEnumerable<Process>> GetPrevStepEnjazProcessesAsync(Expression<Func<Process, bool>> predicate);
}