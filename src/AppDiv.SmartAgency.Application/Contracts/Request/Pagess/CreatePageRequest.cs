

namespace AppDiv.SmartAgency.Application.Contracts.Request.Pagess
{
    public class CreatePageRequest
    {

        public string Category { get; set; } = null!;
        public string Link { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string PageContent { get; set; } = null!;

    }
}