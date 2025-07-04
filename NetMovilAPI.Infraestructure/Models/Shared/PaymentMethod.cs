using NetMovilAPI.Infraestructure.Models.SaleModels;
using System.ComponentModel.DataAnnotations;

namespace NetMovilAPI.Infraestructure.Models.Shared;

public class PaymentMethod : Auditable
{
    [Key]
    public int PaymentMethodID { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public List<SalePayment> SalePayments { get; set; } = new();
}
