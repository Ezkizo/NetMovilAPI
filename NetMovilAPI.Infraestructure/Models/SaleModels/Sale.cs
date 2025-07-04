using NetMovilAPI.Infraestructure.Models.OrderModels;
using NetMovilAPI.Infraestructure.Models.Statuses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetMovilAPI.Infraestructure.Models.SaleModels;
public class Sale : Auditable
{
    [Key]
    public int SaleID { get; set; }
    public decimal TotalPaid { get; set; }

    // Definir las llaves foráneas
    [ForeignKey("PaymentStatusID")]
    public int PaymentStatusID { get; set; }
    public PaymentStatus PaymentStatus { get; set; }

    [ForeignKey("SaleStatusID")]
    public int SaleStatusID { get; set; }
    public SaleStatus SaleStatus { get; set; }

    [ForeignKey("OrderID")]
    public int OrderID { get; set; }
    public Order Order { get; set; }
    public List<SalePayment> Payments { get; set; } = new();
}
