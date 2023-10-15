using Microsoft.EntityFrameworkCore;

namespace OrdersManagementSystem.Orders.Repositories;

public class OrdersContext : DbContext
{
    public DbSet<Models.Order> Orders { get; set; } = null!;

    public OrdersContext(DbContextOptions<OrdersContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrdersContext).Assembly);
    }
}