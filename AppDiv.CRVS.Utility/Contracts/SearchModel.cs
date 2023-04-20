using System.ComponentModel;

namespace AppDiv.CRVS.Utility.Contracts
{
    public class SearchModel<T> where T : class
    {
        public IEnumerable<T> Entities { get; set; } = new List<T> { };
        public int CurrentPage { get; set; }
        public long MaxPage { get; set; }
        public long TotalItems { get; set; }
        public string SearchKeyWord { get; set; } = String.Empty;
        public int PagingSize { get; set; }
        public Dictionary<string, string> Filters { get; set; } = new Dictionary<string, string> { };
        public Dictionary<string, string[]> ObjectFilters { get; set; } = new Dictionary<string, string[]> { };
        public string SortingColumn { get; set; } = null!;
        public SortingDirection SortingDirection { get; set; }
        public Guid[] Tags { get; set; } = new Guid[] { };

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
