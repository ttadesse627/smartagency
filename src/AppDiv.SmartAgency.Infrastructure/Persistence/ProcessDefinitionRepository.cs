

using AppDiv.SmartAgency.Application.Contracts.DTOs.QuickLinksDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Enums;
using AppDiv.SmartAgency.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AppDiv.SmartAgency.Infrastructure.Persistence;
public class ProcessDefinitionRepository : BaseRepository<ProcessDefinition>, IProcessDefinitionRepository
{
    private readonly SmartAgencyDbContext _context;
    public ProcessDefinitionRepository(SmartAgencyDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<int> CreateProcessAsync(Process process)
    {
        int count = 0;
        if (process != null)
        {
            _context.Processes.Add(process);
            count = await _context.SaveChangesAsync();
        }
        return count;
    }

    public async Task<Guid> GetMinStepPdAsync(Guid processId)
    {
        return await _context.ProcessDefinitions.OrderBy(p => p.Step).Where(p => p.ProcessId == processId).Select(p => p.Id).FirstOrDefaultAsync();
    }

    public async Task<int> GetMaxStepAsync(Guid processId)
    {
        return await _context.ProcessDefinitions.Where(p => p.ProcessId == processId).MaxAsync(p => p.Step);
    }

    public async Task<List<DynamicProcessResponseDTO>> GetDynamicProcesses(Guid id)
    {


        var expiredProcesses = await _context.ApplicantProcesses
         .Include(ap => ap.ProcessDefinition)
          .Where(ap => (ap.ProcessDefinitionId == id) && (ap.Status == ProcessStatus.In) && (DateTime.Compare(ap.Date.AddDays(ap.ProcessDefinition.ExpiryInterval), DateTime.Now) < 0))
          .Select(g => new DynamicProcessResponseDTO
          {
              ProcessDefnitionName = g.ProcessDefinition.Name,
              ApplicantName = g.Applicant.AmharicFullName,
              PassportNumber = g.Applicant.PassportNumber,
              DatePassed = (int)DateTime.UtcNow.Subtract(g.Date.AddDays(g.ProcessDefinition.ExpiryInterval)).TotalDays
          })
                  .ToListAsync();

        return expiredProcesses;
    }
}