namespace AppDiv.SmartAgency.Utility.Contracts;
public class AfroMsgResponseModel
{
    public string acknowledge { get; set; } = string.Empty;
    public AfroResponseObj? response { get; set; }
}