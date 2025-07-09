using NetMovilAPI.Infraestructure.Models.OrderModels;
using NetMovilAPI.Infraestructure.Models.SaleModels;
using System.ComponentModel.DataAnnotations;

namespace NetMovilAPI.Infraestructure.Models.Statuses;
public class PaymentStatus
{
    [Key]
    public int PaymentStatusID { get; set; }
    public string Description { get; set; }
    public List<Sale> Sales { get; set; } = [];
}
