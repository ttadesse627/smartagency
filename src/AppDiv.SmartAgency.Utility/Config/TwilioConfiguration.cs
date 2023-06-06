namespace AppDiv.SmartAgency.Utility.Config
{
    public class TwilioConfiguration
    {
        public const string CONFIGURATION_SECTION = "Twilio";
        public string AccountSid { get; set; }
        public string AuthToken { get; set; }

    }
}
