
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;

namespace AppDiv.SmartAgency.Infrastructure.Persistence;
public class ProcessRepository : BaseRepository<Process>, IProcessRepository
{
    private readonly SmartAgencyDbContext _context;
    public ProcessRepository(SmartAgencyDbContext context) : base(context)
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
}