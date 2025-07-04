public class StockRequestDTO
{
    public int StockID { get; set; }
    public decimal Quantity { get; set; }
    public decimal Threshold { get; set; }
    public int BranchID { get; set; } // NUEVO CAMPO
}