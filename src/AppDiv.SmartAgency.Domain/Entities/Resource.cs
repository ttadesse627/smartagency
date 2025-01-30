
namespace AppDiv.SmartAgency.Domain.Entities;
public class Resource
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
}