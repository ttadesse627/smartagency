

using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Domain.Entities.TicketData
{
    public class TicketRegistration
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime RegisteredDate { get; set; } = DateTime.Now;
        public string? TicketNumber { get; set; }
        public DateTime? FlightDate { get; set; }
        public string? DepartureTime { get; set; }
        public string? Transit { get; set; }
        public string? ArrivalTime { get; set; }
        public string? Remark { get; set; }
        public string? TicketPrice { get; set; }
        public Guid? AirLineId { get; set; }
        public LookUp? AirLine { get; set; }
        public Guid? ApplicantId { get; set; }
        public Applicant? Applicant { get; set; }
    }
}