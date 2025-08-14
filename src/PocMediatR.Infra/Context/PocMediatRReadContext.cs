using Microsoft.EntityFrameworkCore;
using PocMediatR.Domain.Context;
using PocMediatR.Domain.Entities;

namespace PocMediatR.Infra.Context
{
    public class PocMediatrReadContext : DbContext, IPocMediatrReadContext
    {
        public PocMediatrReadContext(DbContextOptions<PocMediatrReadContext> options) : base(options)
        {
        }
        public DbSet<PriceType> PriceTypes { get; set; }
        public DbSet<SchemaVersion> SchemaVersions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PocMediatrWriteContext).Assembly);
        }
    }
}
