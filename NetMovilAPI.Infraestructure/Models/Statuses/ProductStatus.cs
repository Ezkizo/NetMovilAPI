using NetMovilAPI.Infraestructure.Models.ProductModels;
using System.ComponentModel.DataAnnotations;

namespace NetMovilAPI.Infraestructure.Models.Statuses;

public class ProductStatus
{
    [Key]
    public int ProductStatusID { get; set; }
    public string Description { get; set; }
    public List<Product> Products { get; set; } = new();
}
