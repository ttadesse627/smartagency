

using AppDiv.SmartAgency.Application.Contracts.Request.Applicants;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.Applicants;

public class LanguageRequest
{
    public Guid LanguageId { get; set; }
    public LanguageAbilityRequest? Ability { get; set; }
}