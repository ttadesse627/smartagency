using Newtonsoft.Json.Linq;

namespace AppDiv.CRVS.Application.Contracts.DTOs
{
    public class FetchGroupDTO
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public string? Description { get; set; }
    }
}