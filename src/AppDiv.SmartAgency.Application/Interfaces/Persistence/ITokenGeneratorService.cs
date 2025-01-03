namespace AppDiv.SmartAgency.Application.Interfaces
{
    public interface ITokenGeneratorService
    {
        public string GenerateJWTToken((string userId, string userName, IList<string> permissions) userDetails);
    }
}
