namespace AppDiv.SmartAgency.Application.Interfaces
{
    public interface ITokenGeneratorService
    {
        public string GenerateJWTToken((string userId, string userGroupIds) userDetails);
    }
}
