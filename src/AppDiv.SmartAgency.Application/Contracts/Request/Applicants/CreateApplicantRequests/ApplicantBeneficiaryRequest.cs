


namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants.CreateApplicantRequests;
public record ApplicantBeneficiaryRequest
{
    public ICollection<BeneficiaryRequest>? Beneficiaries { get; set; }
}