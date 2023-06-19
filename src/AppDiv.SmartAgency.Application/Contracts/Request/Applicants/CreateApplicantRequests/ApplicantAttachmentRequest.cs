

using AppDiv.SmartAgency.Application.Contracts.Request.Common;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants.CreateApplicantRequests;
public record ApplicantAttachmentRequest
{
    public ICollection<AttachmentFileRequest>? AttachmentFiles { get; set; }
}