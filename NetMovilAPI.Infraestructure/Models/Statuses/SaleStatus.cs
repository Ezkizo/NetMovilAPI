using NetMovilAPI.Infraestructure.Models.SaleModels;
using System.ComponentModel.DataAnnotations;

namespace NetMovilAPI.Infraestructure.Models.Statuses;
public class SaleStatus
{
    [Key]
    public int SaleStatusID { get; set; }
    public string Description { get; set; }
    public List<Sale> Sales { get; set; } = [];
}
