
namespace AppDiv.SmartAgency.Application.Contracts.Request.Orders;
public class SponsorAddressRequest
{
    public string? Street { get; set; }
    public string? PhoneNumber { get; set; }
    public string? HouseNumber { get; set; }
    public string? OfficePhone { get; set; }
    public string? Mobile { get; set; }
    public string? AlternativePhone { get; set; }
    public string? Fax { get; set; }
    public string Adress { get; set; } = null!;
    public string? PostCode { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public Guid? CountryId { get; set; }
    public Guid? CityId { get; set; }

}