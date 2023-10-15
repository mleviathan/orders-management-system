using System.ComponentModel.DataAnnotations;

namespace OrdersManagementSystem.Models;

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
}