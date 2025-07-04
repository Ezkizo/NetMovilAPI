using NetMovilAPI.Domain.Entities.Product;

namespace NetMovilAPI.Domain.Entities.Order;
public class OrderProductEntity
{
    public int OrderProductID { get; set; }
    public int Quantity { get; set; }
    public int OrderID { get; set; }
    public int ProductID { get; set; }
    public ProductEntity? Product { get; set; }
}
