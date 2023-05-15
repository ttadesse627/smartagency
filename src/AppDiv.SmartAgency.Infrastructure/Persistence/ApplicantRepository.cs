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
            serviceResponse.Data = await _context.Applicants.FirstOrDefaultAsync(a=>a.PassportNumber==passportNumber);

            return serviceResponse;
        }

    private static Expression<Func<Applicant, bool>> GenerateSearchExpression(
        string searchTerm,
        PropertyInfo[] properties)
    {
        // Create a parameter expression for the entity
        var entityParam = Expression.Parameter(typeof(Applicant), "e");

        // Create an empty expression that we will add to as we generate the search expression
        Expression searchExpr = Expression.Constant(false);

        // Loop through each property of the entity
        foreach (var property in properties)
        {
            // Check if the property is a string
            if (property.PropertyType == typeof(string))
            {
                // Create an expression that searches for the search term
                // in the current property of the entity
                var propertyValue = Expression.Property(entityParam, property);
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var searchTermExpr = Expression.Constant(searchTerm, typeof(string));
                var searchExprPart = Expression.Call(propertyValue, containsMethod, searchTermExpr);

                // Add the search expression for the current property to the overall search expression
                searchExpr = Expression.Or(searchExpr, searchExprPart);
            }
        }

        // Create a lambda expression that takes an entity and returns a boolean
        // that evaluates the overall search expression we just generated
        var lambdaExpr = Expression.Lambda<Func<Applicant, bool>>(searchExpr, entityParam);

        return lambdaExpr;
    }

}