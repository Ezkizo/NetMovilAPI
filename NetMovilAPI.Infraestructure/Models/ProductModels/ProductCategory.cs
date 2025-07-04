using NetMovilAPI.Infraestructure.Models.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetMovilAPI.Infraestructure.Models.ProductModels;
public class ProductCategory
{
    [ForeignKey("ProductID")]
    public int ProductID { get; set; }
    public Product Product { get; set; }

    [ForeignKey("CategoryID")]
    public int CategoryID { get; set; }
    public Category Category { get; set; }
}

