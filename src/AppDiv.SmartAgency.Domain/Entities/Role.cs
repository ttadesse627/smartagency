

using AppDiv.SmartAgency.Utility.Generics;

namespace AppDiv.SmartAgency.Domain.Entities;
public class Role(int value, string name) : Enumeration<Role>(value, name)
{
    public static readonly Role Register = new(1, "Register");
    public ICollection<Permission> Permissions
    { get; set; }
    public ICollection<ApplicationUser> Users { get; set; }

}