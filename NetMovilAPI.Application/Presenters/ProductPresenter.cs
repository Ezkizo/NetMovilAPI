using NetMovilAPI.Application.Presenters.ViewModels;
using NetMovilAPI.Domain.Entities.Product;
using NetMovilAPI.Domain.Interfaces;

namespace NetMovilAPI.Application.Presenters;
public class ProductPresenter : IPresenter<ProductEntity, ProductViewModel>
{
    public ProductViewModel Present(ProductEntity data)
    {
        return new ProductViewModel
        {
            ProductID = data.ProductID,
            Name = data.Name,
            Description = data.Description,
            SupplierPrice = data.SupplierPrice,
            BasePrice = data.BasePrice,
            ProfitMargin = data.ProfitMargin,
            UnitPrice = data.UnitPrice,
            ImageUrl = data.ImageUrl,
            BarCode = data.BarCode,
            ProductStatusID = data.ProductStatus?.Id ?? 0,
            ProductStatus = data.ProductStatus?.Description ?? string.Empty,
            SupplierID = data.SupplierID,
            IsStock = data.IsStock,
            StockID = data.Stock?.StockID ?? null,
            StockQuantity = data.Stock?.Quantity ?? 0,
            Categories = data.ProductCategories?.Select(c => new CategoryViewModel
            {
                CategoryID = c.CategoryID,
                Name = c.Name,
                ImageUrl = c.ImageUrl
            }).ToList() ?? [],
            CreatedAt = data.CreatedAt,
            CreatedBy = data.CreatedBy
        };
    }

    public IEnumerable<ProductViewModel> Present(IEnumerable<ProductEntity> data) => [.. data.Select(Present)];
}
