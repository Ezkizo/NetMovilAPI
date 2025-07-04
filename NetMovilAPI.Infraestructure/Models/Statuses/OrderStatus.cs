using NetMovilAPI.Infraestructure.Models.OrderModels;
using System.ComponentModel.DataAnnotations;

namespace NetMovilAPI.Infraestructure.Models.Statuses;
public class OrderStatus
{
    [Key]
    public int OrderStatusID { get; set; }
    public string Description { get; set; }
    public List<Order> Orders { get; set; } = [];
}
