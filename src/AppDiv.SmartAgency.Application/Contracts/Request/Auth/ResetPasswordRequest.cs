namespace AppDiv.SmartAgency.Application.Contracts.Request.Auth
{
    public class ResetPasswordRequest
    {
        public string? Otp { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }

    }
}