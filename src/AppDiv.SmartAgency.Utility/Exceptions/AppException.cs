namespace AppDiv.SmartAgency.Utility.Exceptions
{
    public abstract class AppException(string title, string message) : Exception(message)
    {
        public string Title { get; } = title;
    }
}
