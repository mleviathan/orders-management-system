using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrdersManagementSystem.Models;

[Table("Products")]
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

    [Required]
    [ForeignKey("Category")]
    public Guid CategoryId { get; set; }

    public virtual Category Category { get; set; }
    public virtual List<Order> Orders { get; set; }
}