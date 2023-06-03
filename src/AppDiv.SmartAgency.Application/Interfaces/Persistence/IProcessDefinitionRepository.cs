

using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence;
public interface IProcessDefinitionRepository : IBaseRepository<ProcessDefinition>
{
    // public Task<Int32> CreateProcessAsync(Process processDefinition);
}