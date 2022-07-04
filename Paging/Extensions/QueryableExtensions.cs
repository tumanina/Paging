using Microsoft.EntityFrameworkCore;
using Paging.Models;
using System.Linq.Expressions;

namespace Paging.Extensions
{
    public static class QueryableExtensions
    {
        public static PagedList<T> ToPagedList<T, TKey>(this IQueryable<T> query, Expression<Func<T, TKey>> orderExpression, int pageIndex = 1, int pageSize = 20)
            where T : class
            where TKey : new()
        {
            var result = query.ToPagedList(pageIndex, pageSize);
            result.Items = query.GetItems(orderExpression, pageIndex, pageSize).ToList();

            return result;
        }

        public static async Task<PagedList<T>> ToPagedListAsync<T, TKey>(this IQueryable<T> query, Expression<Func<T, TKey>> orderExpression, int pageIndex = 1, int pageSize = 20)
            where T : class
            where TKey : new()
        {
            var result = query.ToPagedList(pageIndex, pageSize);
            result.Items = await query.GetItems(orderExpression, pageIndex, pageSize).ToListAsync();

            return result;
        }

        private static PagedList<T> ToPagedList<T>(this IQueryable<T> query, int pageIndex, int pageSize)
            where T : class
        {
            ValidateParameters(pageIndex, pageSize);

            var result = new PagedList<T>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = query.Count()
            };

            result.TotalPages = (int)Math.Ceiling((double)result.TotalCount / pageSize);

            return result;
        }

        private static IQueryable<T> GetItems<T, TKey>(this IQueryable<T> query, Expression<Func<T, TKey>> orderExpression, int pageIndex, int pageSize)
            where T : class
            where TKey : new()
        {
            return query.OrderBy(orderExpression).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        private static void ValidateParameters(int pageIndex, int pageSize)
        {
            if (pageIndex <= 0 || pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException("PageSize and PageIndex values can not be equal 0 or have negative value.");
            }
        }
    }
}
