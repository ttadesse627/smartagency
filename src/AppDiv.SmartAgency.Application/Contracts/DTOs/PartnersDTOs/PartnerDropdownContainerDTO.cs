namespace AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;
public record PartnerDropdownContainerDTO
{
    public ICollection<GetPartnerDropDownDTO>? partners { get; set; }

}