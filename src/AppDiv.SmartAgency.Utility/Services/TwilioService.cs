



// using AppDiv.CRVS.Utility.Config;
// using AppDiv.CRVS.Utility.Services;
// using Microsoft.Extensions.Options;
// using Twilio.Clients;
// using Twilio.Rest.Api.V2010.Account;
// using Twilio.Types;

// public class TwilioService  : ISmsService
// {
//     private readonly ITwilioRestClient _client;
//     private readonly TwilioConfiguration _twiloConfig;

//     public TwilioService(ITwilioRestClient client , IOptions<TwilioConfiguration> twiloConfig)
//    {
//         _client = client;
//         _twiloConfig = twiloConfig.Value;
//     }
//     public bool SendSms(string  to ,  string body )
//     {
//         var message = MessageResource.Create(
//             to: new PhoneNumber(to),
//             from: new PhoneNumber(_twiloConfig.AccountSid),
//             body: body,
//             client: _client); // pass in the custom client
//         return true;
//     }

//     public async Task<string> SendOtpAsync(string to, string prefix, string postfix, int expiration, int codeLength, int codeType)
//     {
//      return "true";
//     }

//     public async Task SendSMS(string to, string message)
//     {

//     }
// }
