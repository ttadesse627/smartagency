

public record ProcessDetailsResponseDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int Step { get; set; }
    public string? Country { get; set; }
    public bool IsVisaRequired { get; set; }
    public bool EnjazRequired { get; set; }
    public ICollection<ProcessDefinitionResponseDTO>? ProcessDefinitions { get; set; }
}