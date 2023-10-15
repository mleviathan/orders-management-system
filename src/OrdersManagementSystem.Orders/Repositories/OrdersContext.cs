using Microsoft.EntityFrameworkCore;

namespace OrdersManagementSystem.Orders.Repositories;

public class OrdersContext : DbContext
{
    #region DbSets

    public DbSet<Models.Order> Orders { get; set; } = null!;

    public OrdersContext() : base(new DbContextOptions<OrdersContext>())
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrdersContext).Assembly);
    }

    #endregion

    /// <summary>
    /// Return an Order by given ID
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public Models.OperationResult<Models.Order> GetOrder(Guid orderId)
    {
        try
        {
            if (Orders == null)
            {
                return new Models.OperationResult<Models.Order>
                {
                    Message = "Orders table is empty"
                };
            }

            if (Orders.Any(o => o.Id == orderId))
            {
                return new Models.OperationResult<Models.Order>
                {
                    Value = Orders.First(o => o.Id == orderId)
                };
            }

            return new Models.OperationResult<Models.Order>
            {
                Message = $"Order with ID {orderId} not found"
            };
        }
        catch (Exception ex)
        {
            return new Models.OperationResult<Models.Order>
            {
                Message = ex.Message
            };
        }
    }

    /// <summary>
    /// Return a list of Orders filtered by given parameters
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="addressId"></param>
    /// <returns></returns>
    public IEnumerable<Models.Order> GetOrders(Guid? userId, Guid? addressId)
    {
        IEnumerable<Models.Order> orders = Orders;
        if (userId.HasValue)
        {
            orders = Orders.Where(o => o.UserId == userId);
        }
        if (addressId.HasValue)
        {
            orders = Orders.Where(o => o.AddressId == addressId);
        }

        return orders;
    }

    /// <summary>
    /// Create an Order in DB
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public Models.OperationResult<Models.Order> CreateOrder(Models.Order order)
    {
        try
        {
            Orders.Add(order);

            SaveChanges();

            return new Models.OperationResult<Models.Order>
            {
                Value = order
            };
        }
        catch (Exception ex)
        {
            return new Models.OperationResult<Models.Order>
            {
                Message = ex.Message
            };
        }
    }

    /// <summary>
    /// Update an Order in DB
    /// </summary>
    /// <param name="order"></param>
    public bool UpdateOrder(Models.Order order)
    {
        try
        {
            Orders.Update(order);

            int updated = SaveChanges();

            if (updated == 1)
            {
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    /// <summary>
    /// Delete an Order from DB
    /// </summary>
    /// <param name="orderId"></param>
    public bool DeleteOrder(Guid orderId)
    {
        try
        {

            var order = Orders.First(o => o.Id == orderId);

            Orders.Remove(order);

            int deleted = SaveChanges();

            if (deleted == 1)
            {
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            return false;
        }

    }
}