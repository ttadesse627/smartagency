
using AppDiv.SmartAgency.Utility.Config;
using Microsoft.Extensions.Options;
using Twilio.Clients;
using Twilio.Http;
using HttpClient = System.Net.Http.HttpClient;

public class TwilioClient : ITwilioRestClient
{
    private readonly ITwilioRestClient _innerClient;
    public TwilioClient(IOptions<TwilioConfiguration> config, HttpClient httpClient)
    {
        // customize the underlying HttpClient
        httpClient.DefaultRequestHeaders.Add("X-Custom-Header", "CustomTwilioRestClient-Demo");

        _innerClient = new TwilioRestClient(
            config.Value.AccountSid,
            config.Value.AuthToken,
            httpClient: new SystemNetHttpClient(httpClient));
    }

    public Response Request(Request request) => _innerClient.Request(request);
    public Task<Response> RequestAsync(Request request) => _innerClient.RequestAsync(request);
    public string AccountSid => _innerClient.AccountSid;
    public string Region => _innerClient.Region;
    public Twilio.Http.HttpClient HttpClient => _innerClient.HttpClient;


}
