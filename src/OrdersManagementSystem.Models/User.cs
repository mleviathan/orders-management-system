using System.ComponentModel.DataAnnotations;

namespace OrdersManagementSystem.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    public virtual List<Order> Orders { get; set; }
}