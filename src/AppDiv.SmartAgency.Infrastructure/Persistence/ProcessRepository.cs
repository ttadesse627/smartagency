
using System.Linq.Expressions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AppDiv.SmartAgency.Infrastructure.Persistence;
public class ProcessRepository : BaseRepository<Process>, IProcessRepository
{
    private readonly SmartAgencyDbContext _context;
    public ProcessRepository(SmartAgencyDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Int32> GetMaximumStepAsync(Expression<Func<Process, bool>> predicate)
    {

        int maxStep = await _context.Processes.Where(predicate).MaxAsync(pr => pr.Step);
        return maxStep;
    }

    public async Task<bool> GetMinStepProcessesAsync(Guid? processId)
    {
        return await _context.Processes.AnyAsync(pr => pr.Id == processId && pr.Step == _context.Processes.Min(pd => pd.Step));
    }


}