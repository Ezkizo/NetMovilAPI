using NetMovilAPI.Domain.Entities.BaseEntities;
using NetMovilAPI.Domain.Entities.Order;

namespace NetMovilAPI.Domain.Entities.Sale;
public class SaleEntity : AuditableEntity
{
    public int SaleID { get; set; }
    public decimal TotalPaid { get; set; }
    public OrderEntity Order { get; set; }
    public List<SalePaymentEntity> Payments { get; set; }
}
