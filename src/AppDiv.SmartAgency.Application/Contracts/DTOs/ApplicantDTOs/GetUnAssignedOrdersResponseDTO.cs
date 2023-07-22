using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;

public class GetUnAssignedOrdersResponseDTO
{
    public ICollection<GetUnAssignedOrdersDropdownResponseDTO>? Order { get; set; }
}