using Microsoft.AspNetCore.Identity;
using NetMovilAPI.Infraestructure.Models.OrderModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace NetMovilAPI.Infraestructure.Models.UserModels;

public class Employee : Auditable
{
    [Key]
    public int EmployeeID { get; set; }
    public string? ProfileImage { get; set; } = "defaultprofilepicture.png";
    public string? EmergencyContact { get; set; }
    public string? EmergencyContactName { get; set; }

    // Llaves Foráneas
    [ForeignKey("Id")]
    public int Id { get; set; }
    public User? User { get; set; }

    public List<Order> Orders { get; set; }
}
