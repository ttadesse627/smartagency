

namespace AppDiv.SmartAgency.Utility.Services
{
    public interface ISmsService
    {
        public Task<string> SendOtpAsync(string to, string prefix, string postfix, int expiration, int codeLength, int codeType);
        public Task SendSMS(string to, string message);
    }
}
