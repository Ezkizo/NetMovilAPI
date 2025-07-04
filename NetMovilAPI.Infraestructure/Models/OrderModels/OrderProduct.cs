using NetMovilAPI.Infraestructure.Models.ProductModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetMovilAPI.Infraestructure.Models.OrderModels;
public class OrderProduct
{
    [Key]
    public int OrderProductID { get; set; }
    public decimal Quantity { get; set; }

    // Definir las llaves foráneas
    [ForeignKey("OrderID")]
    public int OrderID { get; set; }
    public Order Order { get; set; }

    [ForeignKey("ProductID")]
    public int ProductID { get; set; }
    public Product Product { get; set; }
}
