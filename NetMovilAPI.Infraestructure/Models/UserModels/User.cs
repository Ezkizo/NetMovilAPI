using Microsoft.AspNetCore.Identity;
using NetMovilAPI.Infraestructure.Models.OrderModels;
using NetMovilAPI.Infraestructure.Models.Shared;
using NetMovilAPI.Infraestructure.Models.Statuses;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetMovilAPI.Infraestructure.Models.UserModels;
public class User : IdentityUser<int>
{
    public string? ProfileImage { get; set; } = "defaultprofilepicture.png";
    public string? EmergencyContact { get; set; }
    public string? EmergencyContactName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    // Llaves Foráneas
    [ForeignKey("UserStatusID")]
    public int UserStatusID { get; set; }
    public UserStatus UserStatus { get; set; }

    // Nuevo campo opcional
    [ForeignKey("BranchID")]
    public int? BranchID { get; set; }
    public Branch Branch { get; set; }
    public List<UserAddress> Addresses { get; set; }
    public List<Order> OrdersAsEmployee { get; set; }
    public List<Order> OrdersAsCustomer { get; set; }
}
