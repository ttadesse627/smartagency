namespace AppDiv.CRVS.Application.Exceptions
{
    public class InvalidLoginException : Exception
    {
        public InvalidLoginException(string errors) : base(errors)
        {

        }

    }
}
