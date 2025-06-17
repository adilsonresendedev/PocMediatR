using PocMediatR.Common.Interfaces;
using System.Linq.Dynamic.Core;

namespace PocMediatR.Infra.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Page<T>(this IQueryable<T> query, IPageable pageable)
        {
            return query.Page(pageable._page + 1, pageable._size);
        }

        public static IQueryable<T> Sort<T>(this IQueryable<T> query, ISortable sortable)
        {
            if (string.IsNullOrWhiteSpace(sortable._order))
                return query;

            return query.OrderBy(sortable._order);
        }
    }
}
