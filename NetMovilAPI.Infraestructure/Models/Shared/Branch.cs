using NetMovilAPI.Infraestructure.Models.OrderModels;
using NetMovilAPI.Infraestructure.Models.ProductModels;
using NetMovilAPI.Infraestructure.Models.UserModels;
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
    public List<User> Users { get; set; } = [];
    public List<Order> Orders { get; set; } = [];
    public List<Product> Products { get; set; } = [];
    public List<Stock> Stocks { get; set; } = [];
}