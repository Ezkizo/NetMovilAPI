namespace NetMovilAPI.Application.DTOs.Requests;

public class SaleRequestDTO
{
    public int SaleID { get; set; }
    public decimal TotalPaid { get; set; }
    public int PaymentStatusID { get; set; }
    public int SaleStatusID { get; set; }
    public int OrderID { get; set; }
    public OrderRequestDTO? Order { get; set; }
    public List<SalePaymentRequestDTO>? Payments { get; set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public int UpdatedBy { get; set; }
}

public class SalePaymentRequestDTO
{
    public int SalePaymentID { get; set; }
    public int SaleID { get; set; }
    public decimal Amount { get; set; }
    public int PaymentMethodID { get; set; }
    public string? Reference { get; set; }
    public DateTimeOffset? PaymentDate { get; set; }
    public int PaymentStatusID { get; set; }
}
