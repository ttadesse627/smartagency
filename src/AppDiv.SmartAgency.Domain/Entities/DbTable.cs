

namespace AppDiv.SmartAgency.Domain.Entities;
public class DbTable
{
    public string Name { get; set; } = null!;
    public IEnumerable<DbColumn>? Columns { get; set; }
}