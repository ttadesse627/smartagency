
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Contracts.Request.LookUps
{
    public class CreateLookUpRequest
    {
        public string Id { get; set; }
        public string CategoryId { get; set; }
        public string Value { get; set; }

    }
}