using NetMovilAPI.Infraestructure.Models.UserModels;
using System.ComponentModel.DataAnnotations;

namespace NetMovilAPI.Infraestructure.Models.Statuses;

public class UserStatus
{
    [Key]
    public int UserStatusID { get; set; }
    public string Description { get; set; }
    public List<User> Users { get; set; } = new();
}
