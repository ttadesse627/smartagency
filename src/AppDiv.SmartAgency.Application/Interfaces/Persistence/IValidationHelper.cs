namespace AppDiv.SmartAgency.Application.Interfaces.Persistence
{
    public interface IValidationHelper
    {
        public int CalculateAge(DateTime dateOfBirth);
        public bool IsUnique<T1, T2, T3>(T1 tableName, T2 property, T3 propertyValue);



    }
}