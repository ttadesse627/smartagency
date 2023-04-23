namespace AppDiv.SmartAgency.Application.Interfaces.Persistence
{
    public interface IUserResolverService
    {
        string GetUserEmail();
        string GetUserId();
        string GetLocale();
    }
}
