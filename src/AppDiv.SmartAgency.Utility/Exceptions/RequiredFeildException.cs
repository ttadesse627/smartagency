using Org.BouncyCastle.Tsp;

namespace AppDiv.SmartAgency.Utility.Exceptions
{
    public class RequiredFeildException : Exception
    {
        public string Title { get; }
        public RequiredFeildException(string title)
        {
            Title = title;
        }

        public RequiredFeildException(string title, string message) : base(message)
        {
            Title = title;
        }
        public RequiredFeildException(string title, List<string> messages) : base(ConcatenateMessage(messages))
        {
            Title = title;
        }
        private static string ConcatenateMessage(List<string> messages)
        {
            string newmessage = messages.First();
            foreach (string msg in messages)
            {
                newmessage = $"{newmessage}\n{msg}";
            }
            return newmessage;
        }
    }
}
