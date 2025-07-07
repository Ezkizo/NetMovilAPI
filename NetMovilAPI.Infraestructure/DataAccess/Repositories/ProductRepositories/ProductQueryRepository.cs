using Microsoft.EntityFrameworkCore;
using NetMovilAPI.Domain.Entities.Product;
using NetMovilAPI.Domain.Entities.Shared;
using NetMovilAPI.Domain.Interfaces;
using NetMovilAPI.Infraestructure.Models.ProductModels;
using System.Linq.Expressions;

namespace NetMovilAPI.Infraestructure.DataAccess.Repositories.ProductRepositories;
public class ProductQueryRepository : IQueryRepository<Product, ProductEntity>
{
    private readonly AppDbContext _dbContext;
    public ProductQueryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ProductEntity> GetQueryAsync(Expression<Func<Product, bool>> filter)
    {
        var data = await _dbContext.Product
            .Where(filter)
            .Select(c => new ProductEntity
            {
                ProductID = c.ProductID,
                Name = c.Name,
                Description = c.Description,
                BasePrice = c.BasePrice,
                ProfitMargin = c.ProfitMargin,
                UnitPrice = c.UnitPrice,
                ImageUrl = c.ImageUrl,
                BarCode = c.BarCode,
                ProductStatus = new Domain.Entities.BaseEntities.StatusEntity
                {
                    Id = c.ProductStatus.ProductStatusID,
                    Description = c.ProductStatus.Description
                },
                IsStock = c.IsStock,
                Stock = c.Stock != null
                ? new StockEntity
                {
                    StockID = c.Stock.StockID,
                    Quantity = c.Stock.Quantity,
                    Threshold = c.Stock.Threshold
                }
                : null,
                ProductCategories = c.ProductCategories.Select(pc => new CategoryEntity
                {
                    CategoryID = pc.CategoryID,
                    Name = pc.Category.Name ?? "Default",
                    ImageUrl = pc.Category.ImageUrl ?? "default"
                }).ToList(),
                CreatedAt = c.CreatedAt,
                CreatedBy = c.CreatedBy
            })
            .FirstOrDefaultAsync();

        return data ??= new ProductEntity { ProductID = 0, Description = "No fue posible recuperar los resultados" };
    }

    public async Task<IEnumerable<ProductEntity>> GetQueryEnumerableAsync(Expression<Func<Product, bool>> filter)
    {
        return await _dbContext.Product
           .Where(filter)
            .Select(c => new ProductEntity
            {
                ProductID = c.ProductID,
                Name = c.Name,
                Description = c.Description,
                BasePrice = c.BasePrice,
                ProfitMargin = c.ProfitMargin,
                UnitPrice = c.UnitPrice,
                ImageUrl = c.ImageUrl,
                BarCode = c.BarCode,
                ProductStatus = new Domain.Entities.BaseEntities.StatusEntity
                {
                    Id = c.ProductStatus.ProductStatusID,
                    Description = c.ProductStatus.Description
                },
                IsStock = c.IsStock,
                Stock = c.Stock != null ? new StockEntity
                {
                    StockID = c.Stock.StockID,
                    Quantity = c.Stock.Quantity
                } : null,
                ProductCategories = c.ProductCategories.Select(pc => new CategoryEntity
                {
                    CategoryID = pc.CategoryID,
                    Name = pc.Category.Name ?? "Default",
                    ImageUrl = pc.Category.ImageUrl ?? "default"
                }).ToList(),
                CreatedAt = c.CreatedAt,
                CreatedBy = c.CreatedBy
            })
           .ToListAsync();
    }
}