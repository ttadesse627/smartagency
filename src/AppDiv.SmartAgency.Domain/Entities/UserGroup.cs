
namespace AppDiv.SmartAgency.Domain.Entities
{
    public class UserGroup
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public ICollection<Permission> Permissions { get; set; } = [];
        public ICollection<ApplicationUser> AppUsers { get; set; } = [];
    }
}

