using NetMovilAPI.Domain.Entities.BaseEntities;

namespace NetMovilAPI.Domain.Entities.User;
public class CustomerAddressEntity : AuditableEntity
{
    public int CustomerAddressID { get; set; }
    public int CustomerID { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? DeliveryReferences { get; set; } = "Sin referencias";
    public int? PostalCode { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
}
