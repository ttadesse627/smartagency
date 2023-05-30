

using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs
{
    public class CreateLookUpResponseDTO
    {
        public Guid Id { get; set; }

        public string Category { get; set; }

        public string Value { get; set; }

    }
}