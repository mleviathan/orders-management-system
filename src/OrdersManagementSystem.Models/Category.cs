namespace OrdersManagementSystem.Models;

public class Category
{
    public Guid Id { get; set; }

    public virtual List<Order> Orders { get; set; }
}