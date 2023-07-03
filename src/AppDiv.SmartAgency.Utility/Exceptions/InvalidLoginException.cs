namespace AppDiv.SmartAgency.Utility.Exceptions
{
    public class InvalidLoginException : Exception
    {
        public InvalidLoginException(string errors) : base(errors)
        {

        }

    }
}
