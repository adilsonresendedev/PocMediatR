using MediatR;
using Microsoft.EntityFrameworkCore;
using PocMediatR.Domain.Context;
using PocMediatR.Domain.Entities;
using System.Reflection;

namespace PocMediatR.Infra.Context
{
    public class PocMediatRWriteContext : DbContext, IPocMediatRContext
    {
        private readonly IMediator _mediator;

        public PocMediatRWriteContext(DbContextOptions<PocMediatRWriteContext> options, IMediator mediator)
            : base(options)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
 
        public DbSet<PriceType> PriceTypes  { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            var entitiesWithEvents = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity baseEntity && baseEntity.DomainEvents?.Any() == true)
                .Select(e => (BaseEntity)e.Entity)
                .ToList();

            foreach (var entity in entitiesWithEvents)
            {
                var events = entity.DomainEvents!.ToList();
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
