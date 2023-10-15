using Microsoft.EntityFrameworkCore;

namespace OrdersManagementSystem.Orders.Repositories;

public class OrdersContext : DbContext
{
    public DbSet<Models.Order> Orders { get; set; } = null!;

    public OrdersContext(DbContextOptions<OrdersContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=postgres;Username=postgres;Password=postgresql");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrdersContext).Assembly);
    }
}