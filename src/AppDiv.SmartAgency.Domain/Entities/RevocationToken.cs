

namespace AppDiv.SmartAgency.Domain.Entities;
public class RevocationToken
{
    public Guid Id { get; set; }
    public string Token { get; set; }
    public DateTime ExpirationDate { get; set; }
}