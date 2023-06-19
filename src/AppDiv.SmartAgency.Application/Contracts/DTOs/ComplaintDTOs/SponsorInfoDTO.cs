

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ComplaintDTOs;
public record SponsorInfoDTO
{
    public Guid Id { get; set; }
    public string? OrderNumber { get; set; }
    public string? CustomerName { get; set; }
    public string? VisaNumber { get; set; }
    public string? SponsorName { get; set; }
    public string? HousePhone { get; set; }
    public string? MobilePhone { get; set; }
}