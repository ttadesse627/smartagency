using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Infrastructure.Context;

namespace AppDiv.SmartAgency.Infrastructure.Service
{

    public class ValidationHelper : IValidationHelper
    {
        private readonly
        SmartAgencyDbContext _context;

        public ValidationHelper(SmartAgencyDbContext context)
        {
            _context = context;
        }
        public int CalculateAge(DateTime dateOfBirth)
        {
            return DateTime.Now.Year - dateOfBirth.Year;
        }

        public bool IsUnique<T1, T2, T3>(T1 tableName, T2 property, T3 propertyValue)
        {

            return true;
        }
    }
}