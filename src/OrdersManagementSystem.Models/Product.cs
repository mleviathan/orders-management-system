using System.ComponentModel.DataAnnotations;

namespace OrdersManagementSystem.Models;

public class Product
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public double Price { get; set; }

    public virtual List<Order> Orders { get; set; }
}