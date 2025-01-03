namespace AppDiv.SmartAgency.Utility.Exceptions
{
    public class InvalidLoginException(string errors) : Exception(errors) { }
}
