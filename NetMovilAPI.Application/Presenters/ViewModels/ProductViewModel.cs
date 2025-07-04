using System.Text.Json.Serialization;

namespace NetMovilAPI.Application.Presenters.ViewModels;
public class ProductViewModel
{
    public int ProductID { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal SupplierPrice { get; set; }
    public decimal BasePrice { get; set; }
    public decimal ProfitMargin { get; set; }
    public decimal UnitPrice { get; set; }
    public string? ImageUrl { get; set; }
    public string? BarCode { get; set; }
    public int ProductStatusID { get; set; }
    public string? ProductStatus { get; set; }
    public int? SupplierID { get; set; }
    public bool IsStock { get; set; } = true;
    public int? StockID { get; set; }
    public decimal? StockQuantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public int BranchID { get; set; } // NUEVO CAMPO
    public List<CategoryViewModel> Categories { get; set; } = [];
    [JsonIgnore]
    public List<string> Errors { get; set; } = [];
}

