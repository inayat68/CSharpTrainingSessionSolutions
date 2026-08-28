using System.ComponentModel.DataAnnotations;

namespace CustomerManagementWebApp.Models;

public class Order
{
    public int OrderId { get; set; }

    [Required]
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = "";

    [Required]
    [DataType(DataType.Date)]
    public DateTime OrderDate { get; set; } = DateTime.Today;

    [Required]
    [StringLength(200)]
    public string Product { get; set; } = "";

    [Range(1, 100000)]
    public int Quantity { get; set; }

    [Range(0.01, 100000000)]
    public decimal Amount { get; set; }
}
