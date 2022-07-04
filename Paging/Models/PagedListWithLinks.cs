namespace Paging.Models
{
    public class PagedListWithLinks<T> : PagedList<T>
    {
        public string NextPageUrl { get; set; }
        public string PreviousPageUrl { get; set; }
    }
}
