using Microsoft.AspNetCore.Identity;
using NetMovilAPI.Infraestructure.Models.OrderModels;
using NetMovilAPI.Infraestructure.Models.Shared;
using NetMovilAPI.Infraestructure.Models.Statuses;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetMovilAPI.Infraestructure.Models.UserModels;
public class User : IdentityUser<int>
{
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
    public List<Order> Orders { get; set; }
}
