using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;

namespace OrdersManagementSystem.Models;

[Table("Orders")]
public class Order
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey("User")]
    public Guid UserId { get; set; }

    [Required]
    [ForeignKey("Address")]
    public Guid AddressId { get; set; }

    public virtual Address Address { get; set; }
    public virtual User User { get; set; }
    public virtual List<Product> Products { get; set; }
}