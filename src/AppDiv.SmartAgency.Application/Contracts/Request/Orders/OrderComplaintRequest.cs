

namespace AppDiv.SmartAgency.Application.Contracts.Request.Orders
{
    public class OrderComplaintRequest
    {
        public string Message { get; set; }
        public Guid OrderId { get; set; }
    }
}