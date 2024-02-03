

namespace AppDiv.SmartAgency.Application.Contracts.Request.Orders
{
    public class OrderComplaintRequest
    {
        public string Message { get; set; } = null!;
        public Guid ApplicantId { get; set; }
    }
}