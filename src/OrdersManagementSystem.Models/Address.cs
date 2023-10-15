using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrdersManagementSystem.Models;

[Table("Addresses")]
public class Address
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Street { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public string Country { get; set; }

    public virtual Order Order { get; set; }
}