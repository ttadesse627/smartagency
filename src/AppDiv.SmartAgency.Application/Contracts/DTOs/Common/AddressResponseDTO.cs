using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.Common;

public class AddressResponseDTO
{
    public Guid Id { get; set; }
    public LookUpItemResponseDTO? Country { get; set; }
    public LookUpItemResponseDTO? Region { get; set; }
    public string? SubCity { get; set; }
    public string? Zone { get; set; }
    public string? Woreda { get; set; }
    public string? Kebele { get; set; }
    public string? PhoneNumber { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? Website { get; set; }
}