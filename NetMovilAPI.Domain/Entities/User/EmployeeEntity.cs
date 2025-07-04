using NetMovilAPI.Domain.Entities.BaseEntities;
using NetMovilAPI.Domain.Entities.Order;

namespace NetMovilAPI.Domain.Entities.User;
public class EmployeeEntity : AuditableEntity
{
    public int EmployeeID { get; set; }
    public string? ProfileImage { get; set; } = "defaultprofilepicture.webp";
    public string? EmergencyContact { get; set; }
    public string? EmergencyContactName { get; set; }
    public UserEntity? User { get; set; }
    public List<OrderEntity> Orders { get; set; } = new();
}
