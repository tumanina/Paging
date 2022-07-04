using Paging.Models;

namespace Paging.Extensions
{
    public static class PagedListExtensions
    {
        public static PagedList<T2> Convert<T1, T2>(this PagedList<T1> source, Func<T1, T2> mappingMethod)
            where T1 : class
            where T2 : class
        {
            return new PagedList<T2>
            {
                PageIndex = source.PageIndex,
                TotalPages = source.TotalPages,
                PageSize = source.PageSize,
                TotalCount = source.TotalCount,
                Items = source.Items.Select(mappingMethod).ToList()
            };
        }

        public static PagedListWithLinks<T> ConvertToListWithLinks<T>(this PagedList<T> source, string nextPageUrl, string previousPageUrl)
            where T : class
        {
            return new PagedListWithLinks<T>
            {
                PageIndex = source.PageIndex,
                TotalPages = source.TotalPages,
                PageSize = source.PageSize,
                TotalCount = source.TotalCount,
                Items = source.Items,
                NextPageUrl = nextPageUrl,
                PreviousPageUrl = previousPageUrl
            };
        }

        public static int? GetIndexOfPreviousPage<T>(this PagedList<T> page)
            where T : class
        {
            if (page.PageIndex > 1)
            {
                return page.PageIndex - 1;
            }
            return null;
        }

        public static int? GetIndexOfNextPage<T>(this PagedList<T> page)
            where T : class
        {
            if (page.PageIndex < page.TotalPages)
            {
                return page.PageIndex + 1;
            }
            return null;
        }
    }
}
