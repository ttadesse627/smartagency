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


    public async Task<List<Applicant>> SearchEntitiesAsync(string searchTerm, int pageNumber, int pageSize, string sortField, bool sortOrderAscending)
    {
        // Get a reference to the DbSet for the entity we want to search
        var entities = _context.Applicants.AsQueryable();
        Console.WriteLine(entities.ToArray());
        Console.WriteLine("Hey! come on!!!!!");

        // Apply the search term filter if one was provided
        if (!string.IsNullOrEmpty(searchTerm))
        {
            // Get the properties of the entity using reflection
            var entityProperties = typeof(Applicant).GetProperties();

            // Generate a search expression that searches for the search term
            // in any property of the entity that is a string
            var searchExpression = GenerateSearchExpression(searchTerm, entityProperties);

            // Apply the search expression to the entities
            entities = entities.Where(searchExpression);
        }

        // Apply the sorting criteria if they were provided
        if (!string.IsNullOrEmpty(sortField))
        {
            // Use reflection to get the property info for the sort field
            var sortProperty = typeof(Applicant).GetProperty(sortField);

            // Use reflection to create a lambda expression that selects the value of the sort property
            var entityParam = Expression.Parameter(typeof(Applicant), "e");
            var sortExpr = Expression.Property(entityParam, sortProperty);
            var lambdaExpr = Expression.Lambda(sortExpr, entityParam);

            // Use the appropriate OrderBy method based on the sort order
            if (sortOrderAscending)
            {
                entities = Queryable.OrderBy(entities, (dynamic)lambdaExpr);
            }
            else
            {
                entities = Queryable.OrderByDescending(entities, (dynamic)lambdaExpr);
            }
        }

        // Apply the pagination criteria
        entities = entities.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        // Execute the query and return the results
        return await entities.ToListAsync();
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