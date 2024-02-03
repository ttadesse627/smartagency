

namespace AppDiv.SmartAgency.Domain.Entities;
public class Category
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public ICollection<LookUp> LookUps { get; set; } = null!;
}