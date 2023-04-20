namespace AppDiv.CRVS.Utility.Config
{
    public class SMTPServerConfiguration
    {
        public const string CONFIGURATION_SECTION = "SMTP_SERVER";
        public string SMTP_HOST_ADDRESS { get; set; } = null!;
        public short PORT_NON_SSL_OR_TLS { get; set; }
        public short PORT_SSL_OR_TLS { get; set; }
        public string USER_NAME { get; set; } = null!;
		public string SECRET { get; set; } = null!;
		public bool USE_SSL { get; set; }
        public int TIMEOUT_IN_MS { get; set; } // in millisecond
    }
}
