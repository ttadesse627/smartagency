using System.ComponentModel;
using System.Text.Json.Serialization;

namespace AppDiv.SmartAgency.Utility.Contracts
{
    public class SearchModel<T> where T : class
    {
        public IEnumerable<T> Items { get; set; } = new List<T> { };
        public int CurrentPage { get; set; }
        public long MaxPage { get; set; }
        public long TotalCount { get; set; }
        public string SearchKeyWord { get; set; } = String.Empty;
        public int PagingSize { get; set; }
        public Dictionary<string, string> Filters { get; set; } = new Dictionary<string, string> { };
        public Dictionary<string, string[]> ObjectFilters { get; set; } = new Dictionary<string, string[]> { };
        public string SortingColumn { get; set; } = null!;
        public SortingDirection SortingDirection { get; set; }
        public string[] Tags { get; set; } = new string[] { };

        public int GetPageSize()
        {
            if (PagingSize == 0)
            {
                return 15;
            }
            else
            {
                return PagingSize;
            }
        }

        public int GetCurrentPage()
        {
            return CurrentPage;
        }
    }
    public class Paginator
    {
        public int CurrentPage { get; set; }
        public int MaxPage { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SortingDirection
    {
        Ascending = 1,
        Descending = 2
    }

    public enum NavigationPropertyType
    {
        [Description("REFERENCE")]
        REFERENCE,
        [Description("COLLECTION")]
        COLLECTION
    }
}
