
using System.Reflection.PortableExecutable;
using AppDiv.SmartAgency.Application.Contracts.DTOs.QuickLinksDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Enums;
using AppDiv.SmartAgency.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

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

    public async Task<List<Guid>> GetMinStepAsync(Guid processId)
    {
        return await _context.ProcessDefinitions.Where(prd => prd.ProcessId == processId && prd.Step == _context.ProcessDefinitions.Min(pd => pd.Step)).Select(pd => pd.Id).ToListAsync();
    }


    // public async Task<List<Object>> GetDashbourd()
    // {
    //     var response= await _context.ProcessDefinitions
    //       .Include(pd=>pd.ApplicantProcesses)
    //       .FirstOrDefaultAsync(pd=> pd.ApplicantProcesses!=null);
    //       return null;
    // }

    public async Task<List<DynamicProcessResponseDTO>> GetDynamicProcesses(Guid id)
    {
        var expiredProcesses = await _context.ApplicantProcesses
          .Include(ap => ap.ProcessDefinition.Process)
          .Include(ap => ap.ProcessDefinition)
          .Where(ap => DateTime.UtcNow > ap.Date.AddDays(ap.ProcessDefinition.ExpiryInterval))
          .GroupBy(ap => ap.ProcessDefinition.ProcessId)
          .Select(g => new DynamicProcessResponseDTO
          {
              ApplicantName = g.FirstOrDefault().Applicant.AmharicFullName,
              PassportNumber = g.FirstOrDefault().Applicant.PassportNumber,
              Status = g.FirstOrDefault().ProcessDefinition.Name,
              // DatePassed=   (int)(DateTime.Now - g.FirstOrDefault().ProcessDefinition.ApplicantProcesses.FirstOrDefault().Date.Add(g.FirstOrDefault().ProcessDefinition.ExpiryInterval)).TotalDays,  
          })
                  .ToListAsync();

        var expiredProcesses = await _context.ApplicantProcesses
         .Include(ap => ap.ProcessDefinition)
          .Where(ap => (ap.ProcessDefinitionId == id) && (ap.Status == ProcessStatus.In) && (DateTime.UtcNow > ap.Date.AddDays(ap.ProcessDefinition.ExpiryInterval)))
          .Select(g => new DynamicProcessResponseDTO
          {
              ProcessDefnitionName = g.ProcessDefinition.Name,
              ApplicantName = g.Applicant.FirstName + " " + g.Applicant.MiddleName + " " + g.Applicant.LastName,
              PassportNumber = g.Applicant.PassportNumber,
              DatePassed = (int)(DateTime.UtcNow - g.Date.AddDays(g.ProcessDefinition.ExpiryInterval)).TotalDays,
          })
                  .ToListAsync();

        return expiredProcesses;



        return expiredProcesses;
    }
}