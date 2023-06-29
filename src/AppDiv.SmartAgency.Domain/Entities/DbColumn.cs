

namespace AppDiv.SmartAgency.Domain.Entities;
public class DbColumn
{
    public string Name { get; set; }
    public Type Type { get; set; }
    public bool IsForeignKey { get; set; }
    public string? PrincipalEntity { get; set; }
}