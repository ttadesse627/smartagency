namespace AppDiv.SmartAgency.Application.Contracts.DTOs.GroupDTOs
{
    public record GroupDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<PermissionDto> Permissions { get; set; } = [];
    }
}