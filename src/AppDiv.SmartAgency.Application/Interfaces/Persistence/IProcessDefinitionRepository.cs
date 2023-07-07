

using AppDiv.SmartAgency.Application.Contracts.DTOs.QuickLinksDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence;
public interface IProcessDefinitionRepository : IBaseRepository<ProcessDefinition>
{
    // public Task<Int32> CreateProcessAsync(Process processDefinition);
    //public Task<List<Object>>  GetDashbourd();
    public Task<Guid> GetMinStepAsync(Guid processId);
    public Task<List<DynamicProcessResponseDTO>> GetDynamicProcesses(Guid id);
}