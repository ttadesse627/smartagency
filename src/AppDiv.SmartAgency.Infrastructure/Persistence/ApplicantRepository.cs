using System.Linq.Expressions;
using System.Reflection;
using AppDiv.SmartAgency.Application.Common;
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
    public async Task<Int32> CreateApplicantAsync(Applicant applicant, CancellationToken cancellationToken)
    {
        await base.InsertAsync(applicant, cancellationToken);        // await _context.Applicants.AddAsync(applicant);
        var success = await _context.SaveChangesAsync();
        return success;
    }

    public async Task<Applicant> GetApplicantByIdAsync(Guid id)
    {
        var applicant = await _context.Applicants.Where(appl => appl.Id.Equals(id)).FirstOrDefaultAsync(appl => appl.Id == id);
        if (applicant is null)
        {
            throw new Exception($"An applicant with id {id} could not be found!");
        }
        return applicant;
    }

    public async Task<ServiceResponse<Int32>> SaveDbUpdateAsync()
    {
        var response = new ServiceResponse<Int32>();
        try
        {
            response.Data = await _context.SaveChangesAsync();
            response.Message = "The update saved successfully";
            
        }
        catch (Exception ex)
        {
            response.Data = 0;
            response.Message = ex.Message;
            response.Success = false;
            response.Errors?.Add(ex.Message);
        }
        return response;
    }

    public async Task<ServiceResponse<Int32>> DeleteApplicantAsync()
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

    public async Task<List<Applicant>> GetAll()
    {
        var response = new List<Applicant>();
        var applicants = await _context.Applicants.ToListAsync();

        if (applicants.Count >= 0)
        {
            response = applicants;
        }
        return response;
    }
    public async Task<ServiceResponse<Applicant>> GetApplicantByPassportNumber(string passportNumber)
    {
        var serviceResponse = new ServiceResponse<Applicant>();
        serviceResponse.Data = await _context.Applicants.FirstOrDefaultAsync(a => a.PassportNumber == passportNumber);

        return serviceResponse;
    }
    public async Task<int> AddApplicantAsync(Applicant applicant)
    {
        var response = 0;
        try
        {
            await _context.Applicants.AddAsync(applicant);
            response = 1;
        }
        catch (Exception ex)
        {
            throw new Exception("Couldn't add the applicant");
        }

        return response;
    }

    public async Task<int> EditApplicantAsync(Applicant applicant)
    {
        var response = 0;
        try
        {
            _context.Set<Applicant>().Attach(applicant);
            _context.Entry(applicant).State = EntityState.Modified;
            // _context.Applicants.Update(applicant);
            response = await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Cannot update the applicant: {ex.Message}");
        }
        return response;
    }
}