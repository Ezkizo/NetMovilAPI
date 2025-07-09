using Microsoft.EntityFrameworkCore;
using NetMovilAPI.Domain.Entities.BaseEntities;
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
        .Select(p => new ProductEntity
        {
            ProductID = p.ProductID,
            Name = p.Name,
            Description = p.Description,
            BasePrice = p.BasePrice,
            ProfitMargin = p.ProfitMargin,
            UnitPrice = p.UnitPrice,
            ImageUrl = p.ImageUrl,
            BarCode = p.BarCode,
            ProductStatus = new StatusEntity
            {
                Id = p.ProductStatus.ProductStatusID,
                Description = p.ProductStatus.Description
            },
            IsStock = p.IsStock,
            Stock = p.Stock == null ? null : new StockEntity
            {
                StockID = p.Stock.StockID,
                Quantity = p.Stock.Quantity,
                Threshold = p.Stock.Threshold
            },
            ProductCategories = p.ProductCategories
                .Select(pc => new CategoryEntity
                {
                    CategoryID = pc.Category.CategoryID,
                    Name = pc.Category.Name ?? "Default",
                    ImageUrl = pc.Category.ImageUrl ?? "default"
                })
                .ToList(),
            CreatedAt = p.CreatedAt,
            CreatedBy = p.CreatedBy,
            UpdatedBy = p.UpdatedBy,
            UpdatedAt = p.UpdatedAt
        })
        .AsNoTracking() // Mejor rendimiento para consultas de solo lectura
        .FirstOrDefaultAsync();

        return data ??= new ProductEntity { ProductID = 0, Description = "No fue posible recuperar los resultados" };
    }

    public async Task<IEnumerable<ProductEntity>> GetQueryEnumerableAsync(Expression<Func<Product, bool>> filter)
    {
        return await _dbContext.Product
           .Where(filter)
           .Select(p => new ProductEntity
           {
               ProductID = p.ProductID,
               Name = p.Name,
               Description = p.Description,
               BasePrice = p.BasePrice,
               ProfitMargin = p.ProfitMargin,
               UnitPrice = p.UnitPrice,
               ImageUrl = p.ImageUrl,
               BarCode = p.BarCode,
               ProductStatus = new StatusEntity
               {
                   Id = p.ProductStatus.ProductStatusID,
                   Description = p.ProductStatus.Description
               },
               IsStock = p.IsStock,
               Stock = p.Stock == null ? null : new StockEntity
               {
                   StockID = p.Stock.StockID,
                   Quantity = p.Stock.Quantity,
                   Threshold = p.Stock.Threshold
               },
               ProductCategories = p.ProductCategories
                   .Select(pc => new CategoryEntity
                   {
                       CategoryID = pc.Category.CategoryID,
                       Name = pc.Category.Name ?? "Default",
                       ImageUrl = pc.Category.ImageUrl ?? "default"
                   })
                   .ToList(),
               CreatedAt = p.CreatedAt,
               CreatedBy = p.CreatedBy,
               UpdatedBy = p.UpdatedBy,
               UpdatedAt = p.UpdatedAt
           })
           .AsNoTracking() // Mejor rendimiento para consultas de solo lectura
           .ToListAsync();
    }
}