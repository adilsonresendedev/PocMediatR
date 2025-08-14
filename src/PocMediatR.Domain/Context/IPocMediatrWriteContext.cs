using Microsoft.EntityFrameworkCore;
using PocMediatR.Domain.Entities;

namespace PocMediatR.Domain.Context
{
    public interface IPocMediatrWriteContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        //DbSet<Product> Products { get; set; }
        //DbSet<Category> Categories { get;set; }
        DbSet<PriceType> PriceTypes { get; set; }
        //DbSet<Price> Prices { get; set; }
    } 
}
