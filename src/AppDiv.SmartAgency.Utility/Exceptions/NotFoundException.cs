namespace AppDiv.SmartAgency.Utility.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base() { }
        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, Exception exp) : base(message, exp) { }

        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" with a key ({key}) was not found.") { }
    }
}
