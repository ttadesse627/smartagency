
using System.Linq.Expressions;
using AppDiv.SmartAgency.Infrastructure.Context;

public class SearchEntityRepository<TEntity> where TEntity : class
{
    private readonly SmartAgencyDbContext _context;

    public SearchEntityRepository(SmartAgencyDbContext context)
    {
        _context = context;
    }

    public IQueryable<TEntity> Search(string searchTerm = "", string columnName = null)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return _context.Set<TEntity>();
        }

        var parameter = Expression.Parameter(typeof(TEntity), "x");
        var body = Expression.Equal(Expression.Constant(""), Expression.Constant("")); // initial binary expression

        if (string.IsNullOrWhiteSpace(columnName))
        {
            // If no column name is specified, search all string properties
            var stringProperties = typeof(TEntity).GetProperties()
                .Where(p => p.PropertyType == typeof(string))
                .ToList();

            foreach (var prop in stringProperties)
            {
                var propertyExpr = Expression.Property(parameter, prop);
                var containsExpr = Expression.Call(
                                    propertyExpr,
                                    typeof(string).GetMethod("Contains", new[] { typeof(string) }),
                                    Expression.Constant(searchTerm));

                var binaryExpr = Expression.Equal(containsExpr, Expression.Constant(true));
                body = Expression.Or(body, binaryExpr);
            }
        }
        else
        {
            // Search specified column name only
            var prop = typeof(TEntity).GetProperty(columnName);

            if (prop?.PropertyType != typeof(string))
            {
                throw new ArgumentException($"Column '{columnName}' is not a string property");
            }

            var propertyExpr = Expression.Property(parameter, prop);
            var containsExpr = Expression.Call(
                propertyExpr,
                typeof(string).GetMethod("Contains", new[] { typeof(string) }),
                Expression.Constant(searchTerm));
            body = Expression.Equal(containsExpr, Expression.Constant(true));
        }

        var lambda = Expression.Lambda<Func<TEntity, bool>>(body, parameter);
        return _context.Set<TEntity>().Where(lambda);
    }
}