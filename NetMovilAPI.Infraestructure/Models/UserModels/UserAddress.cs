using NetMovilAPI.Infraestructure.Models.OrderModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetMovilAPI.Infraestructure.Models.UserModels;

public class UserAddress : Auditable
{
    [Key]
    public int UserAddressID { get; set; }

    [ForeignKey("UserID")]
    public int UserID { get; set; }
    public User? User { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? References { get; set; } = "Sin referencias";
    public int? PostalCode { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }

}

