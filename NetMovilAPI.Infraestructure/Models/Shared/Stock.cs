using NetMovilAPI.Infraestructure.Models.ProductModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetMovilAPI.Infraestructure.Models.Shared;
public class Stock
{
    [Key]
    public int StockID { get; set; }
    public decimal Quantity { get; set; }
    public decimal Threshold { get; set; }

    [ForeignKey("ProductID")]
    public int ProductID { get; set; }
    public Product Product { get; set; }
}
