using NetMovilAPI.Domain.Entities.BaseEntities;
using NetMovilAPI.Domain.Entities.Shared;

namespace NetMovilAPI.Domain.Entities.Product;
public class ProductEntity : AuditableEntity
{
    public int ProductID { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal BasePrice { get; set; }
    public decimal ProfitMargin { get; set; }
    public decimal UnitPrice { get; set; }
    public string ImageUrl { get; set; } = "defaultproductimage.png";
    public string? BarCode { get; set; } = "Sin Código de Barras";
    public bool IsStock { get; set; }
    public StockEntity? Stock { get; set; } = new();
    public StatusEntity ProductStatus { get; set; } = new();
    public List<CategoryEntity> ProductCategories { get; set; } = [];
    public List<string>? Errors { get; set; } = [];
    public int BranchID { get; set; } // NUEVO CAMPO
}
