namespace AppDiv.CRVS.Utility.Config
{
	public class RabbitMQConfiguration
	{
		public static string CONFIGURATION_SECTION { get; set; } = "RabbitMQ";
		public string Address { get; set; } = null!;
		public string USER_NAME { get; set; } = null!;
		public string SECRET { get; set; } = null!;
	}
}
