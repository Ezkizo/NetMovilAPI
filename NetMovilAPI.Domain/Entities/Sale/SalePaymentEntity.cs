using NetMovilAPI.Domain.Entities.BaseEntities;

namespace NetMovilAPI.Domain.Entities.Sale;
public class SalePaymentEntity : AuditableEntity
{
    public int SalePaymentID { get; set; }
    public decimal Amount { get; set; }
    public string? Reference { get; set; }
    public int SaleID { get; set; }
    public int PaymentMethodID { get; set; }
    public string PaymentMehod { get; set; } = "Desconocido"; // Default value if not set
    public int PaymentStatusID { get; set; }
    public string PaymentStatus { get; set; } = "Desconocido"; // Default value if not set
}
