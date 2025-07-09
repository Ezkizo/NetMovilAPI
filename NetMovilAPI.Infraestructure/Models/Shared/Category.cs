using NetMovilAPI.Infraestructure.Models.Statuses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetMovilAPI.Infraestructure.Models.Shared;
public class Category : Auditable
{
    [Key]
    public int CategoryID { get; set; }
    [MaxLength(70)]
    public string Name { get; set; }
    [MaxLength(300)]
    public string Description { get; set; }
    public string ImageUrl { get; set; }

    [ForeignKey("CategoryStatusID")]
    public int CategoryStatusID { get; set; }
    public CategoryStatus CategoryStatus { get; set; }
}
