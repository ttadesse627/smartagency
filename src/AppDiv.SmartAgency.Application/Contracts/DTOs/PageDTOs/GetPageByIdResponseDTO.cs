namespace AppDiv.SmartAgency.Application.Contracts.DTOs.PageDTOs
{
    public class GetPageByIdResponseDTO
    {
        public Guid Id { get; set; }
        public string? Category { get; set; }
        public string? Link { get; set; }
        public string? Title { get; set; }
        public string? PageContent { get; set; }

    }
}