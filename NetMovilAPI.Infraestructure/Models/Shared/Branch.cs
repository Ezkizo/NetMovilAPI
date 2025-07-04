using NetMovilAPI.Infraestructure.Models.OrderModels;
using NetMovilAPI.Infraestructure.Models.ProductModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetMovilAPI.Infraestructure.Models.Shared;

public class Branch
{
    [Key]
    public int BranchID { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [MaxLength(200)]
    public string? Address { get; set; }

    // Relaciones de navegación
    public List<Order> Orders { get; set; } = new();
    public List<Product> Products { get; set; } = new();
    public List<Stock> Stocks { get; set; } = new();
}