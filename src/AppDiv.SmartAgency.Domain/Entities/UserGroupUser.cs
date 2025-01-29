using System.ComponentModel.DataAnnotations.Schema;

namespace AppDiv.SmartAgency.Domain.Entities;
public class UserGroupUser
{
    public Guid UserGroupId { get; set; } = Guid.NewGuid();
    public UserGroup UserGroup { get; set; } = null!;

    [Column(TypeName = "varchar(255)")]
    public string AppUserId { get; set; } = Guid.NewGuid().ToString();
    public ApplicationUser AppUser { get; set; } = null!;
}