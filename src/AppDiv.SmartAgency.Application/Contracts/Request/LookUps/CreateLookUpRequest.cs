
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Contracts.Request.LookUps
{
    public class CreateLookUpRequest
    {
        public Guid CategoryId { get; set; }
        public string Value { get; set; }

    }
}