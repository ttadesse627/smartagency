namespace AppDiv.SmartAgency.Application.Interfaces.Persistence
{
    public interface IUserResolverService
    {
        string? GetUserEmail();
        Guid GetUserId();
        string? GetLocale();
        Guid GetId();
    }
}
