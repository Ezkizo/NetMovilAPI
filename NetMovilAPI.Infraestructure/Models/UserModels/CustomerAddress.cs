using NetMovilAPI.Infraestructure.Models.OrderModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetMovilAPI.Infraestructure.Models.UserModels;

public class CustomerAddress : Auditable
{
    [Key]
    public int CustomerAddressID { get; set; }

    [ForeignKey("CustomerID")]
    public int CustomerID { get; set; }
    public Customer? Customer { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? DeliveryReferences { get; set; } = "Sin referencias";
    public int? PostalCode { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }

    // Propiedad de navegación
    public List<Order>? Orders { get; set; }
}

