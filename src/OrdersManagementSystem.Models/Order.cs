using System.ComponentModel.DataAnnotations;

namespace OrdersManagementSystem.Models;

public class Order
{
    [Key]
    public Guid Id { get; set; }

    public virtual Address Address { get; set; }
    public virtual User User { get; set; }
    public virtual List<Product> Products { get; set; }
}