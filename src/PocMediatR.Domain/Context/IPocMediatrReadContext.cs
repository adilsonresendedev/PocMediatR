using Microsoft.EntityFrameworkCore;
using PocMediatR.Domain.Entities;

namespace PocMediatR.Domain.Context
{
    public interface IPocMediatrReadContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DbSet<SchemaVersion> SchemaVersions { get; set; }
        DbSet<PriceType> PriceTypes { get; set; }
    }
}
