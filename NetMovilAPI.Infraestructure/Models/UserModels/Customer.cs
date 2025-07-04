using NetMovilAPI.Infraestructure.Models.OrderModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetMovilAPI.Infraestructure.Models.UserModels;
public class Customer : Auditable
{
    [Key]
    public int CustomerID { get; set; }

    // Llaves Foráneas
    [ForeignKey("Id")]
    public int Id { get; set; }
    public User User { get; set; }
    public List<CustomerAddress> Addresses { get; set; } = new();
    public List<Order> Orders { get; set; } = new();
}
