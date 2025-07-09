using NetMovilAPI.Domain.Entities.BaseEntities;

namespace NetMovilAPI.Domain.Entities.Shared;
public class CategoryEntity : AuditableEntity
{
    public int CategoryID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; } = "/iconos/defaultcategoryicon.webp";
    public StatusEntity CategoryStatus { get; set; }
}
