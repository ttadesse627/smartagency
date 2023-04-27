

namespace AppDiv.SmartAgency.Domain.Entities;
public class Category
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public ICollection<LookUp> LookUps { get; set; }
}