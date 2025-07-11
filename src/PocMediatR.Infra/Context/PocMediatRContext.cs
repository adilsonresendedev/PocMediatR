using MediatR;
using Microsoft.EntityFrameworkCore;
using PocMediatR.Domain.Context;
using PocMediatR.Domain.Entities;
using System.Reflection;

namespace PocMediatR.Infra.Context
{
    public class PocMediatRContext : DbContext, IPocMediatRContext
    {
        private readonly IMediator _mediator;

        public PocMediatRContext(DbContextOptions<PocMediatRContext> options, IMediator mediator)
            : base(options)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        //public DbSet<Product> Products { get; set; }
        //public DbSet<Category> Categories  { get; set; }
        public DbSet<PriceType> PriceTypes  { get; set; }
        //public DbSet<Price> Prices  { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents?.Any() == true)
                .ToList();

            foreach (var entity in entitiesWithEvents)
            {
                var events = entity.DomainEvents.ToList();
                entity.ClearDomainEvents();

                foreach (var domainEvent in events)
                {
                    await _mediator.Publish(domainEvent, cancellationToken);
                }
            }

            return result;
        }
    }
}
