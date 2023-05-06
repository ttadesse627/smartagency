using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Features.Command.Update.Applicants;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AppDiv.SmartAgency.Infrastructure.Persistence;
public class ApplicantRepository : BaseRepository<Applicant>, IApplicantRepository
{
    private readonly SmartAgencyDbContext _context;
    public ApplicantRepository(SmartAgencyDbContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }
    public override async Task InsertAsync(Applicant applicant, CancellationToken cancellationToken)
    {
        await base.InsertAsync(applicant, cancellationToken);
    }
    public async Task<Int32> CreateApplicantAsync(Applicant applicant)
    {
        await _context.Applicants.AddAsync(applicant);
        var success = await _context.SaveChangesAsync();
        return success;
    }


    public async Task<Applicant> GetApplicantAsync(Guid id)
    {
        var applicant = new Applicant();
        applicant = await _context.Applicants.Where(appl => appl.Id.Equals(id)).FirstOrDefaultAsync(appl => appl.Id == id);
        return applicant;
    }

    public async Task<ServiceResponse<Int32>> EditApplicantAsync()
    {
        var response = new ServiceResponse<Int32>();
        try
        {
            response.Data = await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Success = false;
        }
        return response;
    }

    public async Task<ServiceResponse<List<Applicant>>> GetAll()
    {
        var response = new ServiceResponse<List<Applicant>>();
        var applicants = await _context.Applicants.ToListAsync();
        
        if (applicants.Count >= 0)
        {
            response.Data = applicants;
            response.Message = "All data are fetched.";
            response.Success = true;
        }
        else
        {
            response.Message = "No records found!";
            response.Success = false;
        }
        return response;
    }
}