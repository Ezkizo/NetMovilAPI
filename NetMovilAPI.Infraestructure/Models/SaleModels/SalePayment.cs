using NetMovilAPI.Infraestructure.Models.Shared;
using NetMovilAPI.Infraestructure.Models.Statuses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetMovilAPI.Infraestructure.Models.SaleModels;
public class SalePayment : Auditable
{
    [Key]
    public int SalePaymentID { get; set; }
    public decimal Amount { get; set; }
    public string? Reference { get; set; }

    // Definir las llaves foráneas
    [ForeignKey("SaleID")]
    public int SaleID { get; set; }
    public Sale Sale { get; set; }

    [ForeignKey("PaymentMethodID")]
    public int PaymentMethodID { get; set; }
    public PaymentMethod PaymentMethod { get; set; }

    [ForeignKey("PaymentStatusID")]
    public int PaymentStatusID { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}
