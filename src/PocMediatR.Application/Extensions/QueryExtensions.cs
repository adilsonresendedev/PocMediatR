using PocMediatR.Application.Models;
using PocMediatR.Common.Interfaces;
using PocMediatR.Infra.Extensions;
using Microsoft.EntityFrameworkCore;

namespace PocMediatR.Application.Extensions
{
    public static class QueryExtensions
    {
        public static async Task<PagedResponse<T>> GetPageAsync<T>(this IQueryable<T> query, IPageable pageable, CancellationToken cancellationToken) where T : class
        {
            var currentPage = await query
                .Page(pageable)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return new PagedResponse<T>
            {
                Data = currentPage,
                CurrentPage = pageable._page,
                TotalItems = query.Count(),
                Size = pageable._size
            };
        }
       
    }
}
