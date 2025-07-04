using NetMovilAPI.Infraestructure.Models.Shared;
using NetMovilAPI.Infraestructure.Models.Statuses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetMovilAPI.Infraestructure.Models.ProductModels;
public class Product : Auditable
{
    [Key]
    public int ProductID { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    // Añadir campo de precio de proveedor
    public decimal SupplierPrice { get; set; }
    public decimal BasePrice { get; set; }
    public decimal ProfitMargin { get; set; }
    public decimal UnitPrice { get; set; }
    public string? ImageUrl { get; set; } = "defaultproductimage.webp";
    public string? BarCode { get; set; } = "Sin Código de Barras";
    public bool IsStock { get; set; }

    [ForeignKey("StockID")]
    public int? StockID { get; set; }
    public Stock Stock { get; set; }

    [ForeignKey("ProductStatusID")]
    public int ProductStatusID { get; set; }
    public ProductStatus ProductStatus { get; set; }
    public List<ProductCategory> ProductCategories { get; set; } = [];

    public int BranchID { get; set; } // NUEVO CAMPO
}