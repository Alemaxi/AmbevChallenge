using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.ORM;

public class DefaultContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartProduct> CartProducts { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleItem> saleItems { get; set; }

    public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var addedSales = ChangeTracker.Entries<Sale>()
                           .Where(e => e.State == EntityState.Added)
                           .Select(e => e.Entity)
                           .ToList();

        var modifiedSales = ChangeTracker.Entries<Sale>()
                           .Where(e => e.State == EntityState.Modified && !e.Entity.IsCancelled)
                           .Select(e => e.Entity)
                           .ToList();

        var deletedSales = ChangeTracker.Entries<Sale>()
                           .Where(e => e.State == EntityState.Deleted)
                           .Select(e => e.Entity)
                           .ToList();

        var canceledSales = ChangeTracker.Entries<Sale>()
                           .Where(e => e.State == EntityState.Modified && e.Entity.IsCancelled)
                           .Select(e => e.Entity)
                           .ToList();

        var result = base.SaveChangesAsync(cancellationToken);

        OnSaleCanceled(canceledSales);
        OnSaleDeleted(deletedSales);
        OnSaleInserted(addedSales);
        OnSaleModified(modifiedSales);


        return result;
    }

    private void OnSaleInserted(List<Sale> sales)
    {
        foreach (var sale in sales)
        {
            Console.WriteLine($"Sale {sale.Id} inserted");
        }
    }

    private void OnSaleModified(List<Sale> sales)
    {
        foreach (var sale in sales)
        {
            Console.WriteLine($"Sale {sale.Id} modified");
        }
    }

    private void OnSaleDeleted(List<Sale> sales)
    {
        foreach (var sale in sales)
        {
            Console.WriteLine($"Sale {sale.Id} deleted");
        }
    }

    private void OnSaleCanceled(List<Sale> sales)
    {
        foreach (var sale in sales)
        {
            Console.WriteLine($"Sale {sale.Id} canceled");
        }
    }
}
public class YourDbContextFactory : IDesignTimeDbContextFactory<DefaultContext>
{
    public DefaultContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<DefaultContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseNpgsql(
               connectionString,
               b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.WebApi")
        );

        return new DefaultContext(builder.Options);
    }
}