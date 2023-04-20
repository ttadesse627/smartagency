namespace AppDiv.CRVS.Application.Interfaces.Persistence
{
    public interface IUserResolverService
    {
        string GetUserEmail();
        Guid GetUserId();
        string GetLocale();
    }
}
