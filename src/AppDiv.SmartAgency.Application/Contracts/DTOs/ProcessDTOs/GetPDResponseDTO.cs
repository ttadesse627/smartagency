namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
public record GetPDResponseDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int Step { get; set; }
    public bool RequestApproval { get; set; }

}