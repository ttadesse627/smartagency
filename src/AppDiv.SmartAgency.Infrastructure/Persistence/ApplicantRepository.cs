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

    public async Task<Applicant> GetApplicantByIdWithAsync(Guid id)
    {
        var applicant = await _context.Applicants
                                .Include(appl => appl.IssuingCountry)
                                    .ThenInclude(ln => ln.Category)
                                .Include(appl => appl.PassportIssuedPlace)
                                    .ThenInclude(lk => lk.Category)
                                .Include(appl => appl.MaritalStatus)
                                    .ThenInclude(lk => lk.Category)
                                .Include(appl => appl.Health)
                                    .ThenInclude(lk => lk.Category)
                                .Include(appl => appl.Religion)
                                    .ThenInclude(lk => lk.Category)
                                .Include(appl => appl.Jobtitle)
                                    .ThenInclude(lk => lk.Category)
                                .Include(appl => appl.Experience)
                                    .ThenInclude(lk => lk.Category)
                                .Include(appl => appl.Language)
                                    .ThenInclude(lk => lk.Category)
                                .Include(appl => appl.Salary)
                                    .ThenInclude(lk => lk.Category)
                                .Include(appl => appl.DesiredCountry)
                                    .ThenInclude(lk => lk.Category)
                                .Include(appl => appl.BrokerName)
                                    .ThenInclude(lk => lk.Category)
                                .Include(appl => appl.Branch)
                                    .ThenInclude(lk => lk.Category)
                                .Include(appl => appl.Partner)
                                .Include(appl => appl.LanguageSkills)
                                    .ThenInclude(ln => ln.Language)
                                        .ThenInclude(lk => lk.Category)
                                .Include(appl => appl.Skills)
                                    .ThenInclude(lk => lk.Category)
                                .Include(appl => appl.Experiences)
                                    .ThenInclude(exp => exp.Country)
                                        .ThenInclude(lk => lk.Category)
                                .Include(appl => appl.Education)
                                    .ThenInclude(edu => edu.QualificationTypes)
                                        .ThenInclude(lk => lk.Category)
                                .Include(appl => appl.Education)
                                    .ThenInclude(edu => edu.LevelOfQualifications)
                                        .ThenInclude(lk => lk.Category)
                                .Include(appl => appl.Education)
                                    .ThenInclude(edu => edu.Awards)
                                        .ThenInclude(lk => lk.Category)
                                .Include(appl => appl.BankAccount)
                                .Include(appl => appl.EmergencyContact)
                                    .ThenInclude(ec => ec.Relationship)
                                .Include(appl => appl.EmergencyContact)
                                    .ThenInclude(ec => ec.Address)
                                        .ThenInclude(addr => addr.AddressRegion)
                                .Include(appl => appl.Representative)
                                    .ThenInclude(rep => rep.Address)
                                        .ThenInclude(addr => addr.AddressRegion)
                                            .ThenInclude(lk => lk.Category)
                                .Include(appl => appl.Witnesses)
                                .Include(appl => appl.Beneficiaries)
                                    .ThenInclude(ben => ben.Relationship)
                                        .ThenInclude(lk => lk.Category)
                                .Include(appl => appl.AttachmentFiles)
                                    .ThenInclude(atf => atf.Attachment)
                                .Include(appl => appl.Address)
                                    .ThenInclude(addr => addr.AddressRegion)
                                        .ThenInclude(lk => lk.Category)
                                .Where(appl => appl.Id.Equals(id)).FirstOrDefaultAsync(appl => appl.Id == id);
        if (applicant is null)
        {
            throw new Exception($"An applicant with id {id} could not be found!");
        }
        return applicant;
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

    public async Task<int> EditApplicantAsync(Applicant applicant)
    {
        var response = 0;
        try
        {
            _context.Applicants.Update(applicant);
            response = await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Cannot update the applicant: {ex.Message}");
        }
        return response;
    }
}