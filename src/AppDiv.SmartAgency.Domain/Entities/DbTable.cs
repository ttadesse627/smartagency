

namespace AppDiv.SmartAgency.Domain.Entities;
public class DbTable
{
    public string Name { get; set; }
    public IEnumerable<DbColumn>? Columns { get; set; }
}