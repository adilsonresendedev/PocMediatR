using Microsoft.EntityFrameworkCore;
using PocMediatR.Domain.Context;
using PocMediatR.Domain.Entities;
using System.Reflection;

namespace PocMediatR.Infra.Context
{
    public class PocMediatRContext : DbContext, IPocMediatRContext
    {
        public PocMediatRContext(DbContextOptions<PocMediatRContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories  { get; set; }
        public DbSet<PriceType> PriceTypes  { get; set; }
        public DbSet<Price> Prices  { get; set; }
    }
}
