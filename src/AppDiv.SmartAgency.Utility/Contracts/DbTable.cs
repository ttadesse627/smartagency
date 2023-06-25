

namespace AppDiv.SmartAgency.Utility.Contracts;
public class DbTable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public List<DbColumn> Columns { get; set; }
}