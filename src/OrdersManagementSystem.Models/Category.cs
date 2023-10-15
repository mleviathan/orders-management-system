using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrdersManagementSystem.Models;

[Table("Categories")]
public class Category
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    public virtual List<Product> Products { get; set; }
}