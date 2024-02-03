

namespace AppDiv.SmartAgency.Domain.Entities;
public class DbColumn
{
    public string Name { get; set; } = null!;
    public Type Type { get; set; } = null!;
    public bool IsForeignKey { get; set; }
    public string? PrincipalEntity { get; set; }
}