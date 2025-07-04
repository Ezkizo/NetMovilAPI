namespace NetMovilAPI.Domain.Entities.Shared;
public class StockEntity
{
    public int StockID { get; set; }
    public int ProductID { get; set; }
    public decimal Quantity { get; set; }
    public decimal Threshold { get; set; }
}
