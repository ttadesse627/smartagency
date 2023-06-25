

namespace AppDiv.SmartAgency.Utility.Contracts;
public class DbColumn
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public Type Type { get; set; }
}