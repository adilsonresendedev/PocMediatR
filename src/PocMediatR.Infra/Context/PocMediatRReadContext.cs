using Microsoft.EntityFrameworkCore;
using PocMediatR.Domain.Entities;

namespace PocMediatR.Infra.Context
{
    public class PocMediatRReadContext : DbContext
    {
        public PocMediatRReadContext(DbContextOptions<PocMediatRReadContext> options) : base(options)
        {
        }
        public DbSet<PriceType> PriceTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PocMediatRWriteContext).Assembly);
        }
    }
}
