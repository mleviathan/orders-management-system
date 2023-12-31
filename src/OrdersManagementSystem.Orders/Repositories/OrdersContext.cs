using Microsoft.EntityFrameworkCore;

namespace OrdersManagementSystem.Orders.Repositories;

public class OrdersContext : DbContext
{
    #region DbSet

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
    public virtual Models.OperationResult<Models.Order> GetOrder(Guid orderId)
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
    public virtual Models.OperationResult<IEnumerable<Models.Order>> GetOrders(Guid? userId, Guid? addressId)
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

        return new Models.OperationResult<IEnumerable<Models.Order>> { Value = orders };
    }

    /// <summary>
    /// Create an Order in DB
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public virtual Models.OperationResult<Models.Order> CreateOrder(Models.Order order)
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
    public virtual Models.OperationResult<Models.Order> UpdateOrder(Models.Order order)
    {
        try
        {
            if (Orders.Find(order.Id) == null)
            {
                return new Models.OperationResult<Models.Order>
                {
                    Message = $"Order with ID {order.Id} not found"
                };
            }

            Orders.Update(order);

            int updated = SaveChanges();

            if (updated == 1)
            {
                return new Models.OperationResult<Models.Order>
                {
                    Value = order
                };
            }

            return new Models.OperationResult<Models.Order>
            {
                Message = $"An error occured while deleting entity with ID {order.Id}"
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
    /// Delete an Order from DB
    /// </summary>
    /// <param name="orderId"></param>
    public virtual Models.OperationResult<bool> DeleteOrder(Guid orderId)
    {
        try
        {
            if (Orders.Find(orderId) == null)
            {
                return new Models.OperationResult<bool>
                {
                    Message = $"Order with ID {orderId} not found",
                    Value = false
                };
            }

            var order = Orders.First(o => o.Id == orderId);

            Orders.Remove(order);

            int deleted = SaveChanges();

            if (deleted == 1)
            {
                return new Models.OperationResult<bool>
                {
                    Value = true
                };
            }

            return new Models.OperationResult<bool>
            {
                Message = $"An error occured while deleting entity with ID {orderId}",
                Value = false
            };
        }
        catch (Exception ex)
        {
            return new Models.OperationResult<bool>
            {
                Message = ex.Message
            };
        }

    }
}