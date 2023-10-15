namespace OrdersManagementSystem.Models;

public class User
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public virtual List<Order> Orders { get; set; }
}