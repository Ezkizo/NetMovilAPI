public class StockViewModel
{
    public int StockID { get; set; }
    public decimal Quantity { get; set; }
    public decimal Threshold { get; set; }
    public int ProductID { get; set; }
    public string ProductName { get; set; } = null!;
    public int BranchID { get; set; } // NUEVO CAMPO
}