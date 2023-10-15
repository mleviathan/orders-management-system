using System.ComponentModel.DataAnnotations;

namespace OrdersManagementSystem.Models;

public class Category
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    public virtual List<Order> Orders { get; set; }
}