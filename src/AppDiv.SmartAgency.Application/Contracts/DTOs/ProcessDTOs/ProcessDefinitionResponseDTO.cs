public record ProcessDefinitionResponseDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int Step { get; set; }
    public int ExpiryInterval { get; set; }
    public bool RequestApproval { get; set; }

}