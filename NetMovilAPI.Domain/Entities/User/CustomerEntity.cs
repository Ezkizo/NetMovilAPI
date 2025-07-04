using NetMovilAPI.Domain.Entities.BaseEntities;
using NetMovilAPI.Domain.Entities.Order;

namespace NetMovilAPI.Domain.Entities.User;
public class CustomerEntity : AuditableEntity
{
    public int? CustomerID { get; set; }
    public int UserID { get; set; }
    public UserEntity User { get; set; }
    public List<CustomerAddressEntity> Addresses { get; set; }
    public List<OrderEntity> Orders { get; set; }
}
