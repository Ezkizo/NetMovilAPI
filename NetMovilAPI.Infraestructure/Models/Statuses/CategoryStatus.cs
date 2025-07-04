using NetMovilAPI.Infraestructure.Models.Shared;
using System.ComponentModel.DataAnnotations;

namespace NetMovilAPI.Infraestructure.Models.Statuses;

public class CategoryStatus
{
    [Key]
    public int CategoryStatusID { get; set; }
    public string Description { get; set; }
    public List<Category> Categories { get; set; } = [];
}
