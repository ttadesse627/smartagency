namespace AppDiv.SmartAgency.Utility.Config
{
    public class TwilioConfiguration
    {
        public const string CONFIGURATION_SECTION = "Twilio";
        public string AccountSid { get; set; } = string.Empty;
        public string AuthToken { get; set; } = string.Empty;

    }
}
