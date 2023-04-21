

namespace AppDiv.SmartAgency.Domain.Entities;
public class Attachment
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsRequired { get; set; }
    public bool ShowOnCv { get; set; }
}