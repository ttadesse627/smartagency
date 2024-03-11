namespace AppDiv.SmartAgency.Application.Contracts.DTOs.GroupDTOs
{
    public class FetchGroupDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<PermissionDto> Permissions { get; set; } = [];
    }
}