using Microsoft.EntityFrameworkCore;
using NetMovilAPI.Domain.Entities.BaseEntities;
using NetMovilAPI.Domain.Entities.Product;
using NetMovilAPI.Domain.Entities.Shared;
using NetMovilAPI.Domain.Interfaces;
using NetMovilAPI.Infraestructure.Models.ProductModels;
using NetMovilAPI.Infraestructure.Models.Shared;
using System.Diagnostics;

namespace NetMovilAPI.Infraestructure.DataAccess.Repositories.ProductRepositories;

public class ProductActionRepository : IActionRepository<ProductEntity>
{
    private readonly AppDbContext _dbContext;
    public ProductActionRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductEntity> AddAsync(ProductEntity entity)
    {
        // 1. Mapear DTO a entidad con hijos en una sola operación
        var product = new Product
        {
            Name = entity.Name,
            Description = entity.Description,
            BasePrice = entity.BasePrice,
            ProfitMargin = entity.ProfitMargin,
            UnitPrice = entity.UnitPrice,
            ImageUrl = entity.ImageUrl,
            BarCode = entity.BarCode,
            IsStock = entity.IsStock,
            ProductStatusID = entity.ProductStatus.Id,
            BranchID = entity.BranchID,

            // 1.a. Categorías como coleccion de navegación
            ProductCategories = [.. entity.ProductCategories.Select(pc => new ProductCategory { CategoryID = pc.CategoryID })],

            // 1.b. Stock como entidad relacionada
            Stock = entity.IsStock && entity.Stock != null
                ? new Stock
                {
                    Quantity = entity.Stock.Quantity,
                    Threshold = entity.Stock.Threshold,
                    BranchID = entity.BranchID
                }
                : null,

            CreatedAt = DateTime.UtcNow,
            CreatedBy = entity.CreatedBy
        };

        try
        {
            // 2. Agregar la entidad principal incluyendo relaciones
            await _dbContext.Product.AddAsync(product);

            // 3. Un solo SaveChanges para cascada de FK de categorías y stock
            await _dbContext.SaveChangesAsync();

            // 4. Actualizar DTO con valores generados
            entity.ProductID = product.ProductID;
            entity.CreatedAt = product.CreatedAt;
            entity.Stock ??= new StockEntity();
            entity.Stock.StockID = product.Stock?.StockID ?? 0;
            if (product.ProductCategories != null)
                entity.ProductCategories = [.. product.ProductCategories.Select(pc => new CategoryEntity{ CategoryID = pc.CategoryID })];
            return entity;
        }
        catch (DbUpdateException dbEx)
        {
            // Manejo específico de errores de integridad referencial
            Debug.WriteLine($"Error en BD: {dbEx.Message}");
            entity.ProductID = 0;
            entity.Description = "Error al guardar el producto. Verifique datos de categorías o stock.";
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error genérico: {ex.Message}");
            entity.ProductID = 0;
            entity.Description = "Error inesperado al guardar el registro.";
            return entity;
        }
    }

    public async Task<ProductEntity> UpdateAsync(ProductEntity entity)
    {
        try
        {
            // 1. Buscar el producto existente con sus relaciones
            var product = await _dbContext.Product
                .Include(p => p.ProductCategories)
                .Include(p => p.Stock)
                .FirstOrDefaultAsync(p => p.ProductID == entity.ProductID);

            if (product == null)
            {
                entity.Description = "No se encontró el producto para actualizar.";
                return entity;
            }

            // 2. Actualizar propiedades principales
            product.Name = entity.Name;
            product.Description = entity.Description;
            product.BasePrice = entity.BasePrice;
            product.ProfitMargin = entity.ProfitMargin;
            product.UnitPrice = entity.UnitPrice;
            product.ImageUrl = entity.ImageUrl;
            product.BarCode = entity.BarCode;
            product.IsStock = entity.IsStock;
            product.ProductStatusID = entity.ProductStatus.Id;
            product.BranchID = entity.BranchID;
            product.UpdatedAt = DateTime.UtcNow;
            product.UpdatedBy = entity.UpdatedBy;

            // 3. Actualizar categorías (agregar nuevas y quitar eliminadas)
            var newCategoryIds = entity.ProductCategories.Select(pc => pc.CategoryID).ToHashSet();
            var currentCategoryIds = product.ProductCategories.Select(pc => pc.CategoryID).ToHashSet();

            // Categorías a eliminar
            var toRemove = product.ProductCategories.Where(pc => !newCategoryIds.Contains(pc.CategoryID)).ToList();
            foreach (var cat in toRemove)
                product.ProductCategories.Remove(cat);

            // Categorías a agregar
            var toAdd = newCategoryIds.Except(currentCategoryIds);
            foreach (var catId in toAdd)
                product.ProductCategories.Add(new ProductCategory { ProductID = product.ProductID, CategoryID = catId });

            // 4. Actualizar o crear stock si corresponde
            if (entity.IsStock)
            {
                if (product.Stock == null && entity.Stock != null)
                {
                    product.Stock = new Stock
                    {
                        Quantity = entity.Stock.Quantity,
                        Threshold = entity.Stock.Threshold,
                        BranchID = entity.BranchID
                    };
                }
                else if (product.Stock != null && entity.Stock != null)
                {
                    product.Stock.Quantity = entity.Stock.Quantity;
                    product.Stock.Threshold = entity.Stock.Threshold;
                    product.Stock.BranchID = entity.BranchID;
                }
            }
            else
            {
                // Si ya no maneja stock, eliminar el registro de stock si existe
                if (product.Stock != null)
                    _dbContext.Stock.Remove(product.Stock);
            }

            await _dbContext.SaveChangesAsync();

            // 5. Mapear de vuelta a la entidad de dominio
            entity.UpdatedAt = product.UpdatedAt;
            entity.ProductCategories = [.. product.ProductCategories.Select(pc => new CategoryEntity { CategoryID = pc.CategoryID })];

            if (product.Stock != null)
            {
                entity.Stock ??= new StockEntity();
                entity.Stock.StockID = product.Stock.StockID;
                entity.Stock.Quantity = product.Stock.Quantity;
                entity.Stock.Threshold = product.Stock.Threshold;
            }
            else
            {
                entity.Stock = null;
            }

            return entity;
        }
        catch (DbUpdateException dbEx)
        {
            Debug.WriteLine($"Error en BD: {dbEx.Message}");
            entity.Description = "Error al actualizar el producto. Verifique datos de categorías o stock.";
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error genérico: {ex.Message}");
            entity.Description = "Error inesperado al actualizar el registro.";
            return entity;
        }
    }

    public async Task<ApiResponse<ProductEntity>> DeleteAsync(int id, int idUser)
    {
        try
        {

            var query = _dbContext.Product.Where(p => p.ProductID == id);
            var model = await query.FirstOrDefaultAsync();
            if (model != null)
            {
                await query.ExecuteUpdateAsync(p => p.SetProperty(pu => pu.ProductStatusID, 1)
                        .SetProperty(pu => pu.DeletedAt, DateTime.Now)
                        .SetProperty(pu => pu.DeletedBy, idUser));
                return new ApiResponse<ProductEntity>
                {
                    Success = true,
                    Message = "Producto eliminado con exito."
                };
            }
            else
            {
                return new ApiResponse<ProductEntity>
                {
                    Success = false,
                    Message = "No se encontró el producto."
                };
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error: {ex.Message}");
            return new ApiResponse<ProductEntity>
            {
                Success = false,
                Message = "Ocurrio un error al guardar el registro, si el problema persiste contacte al administrador."
            };
        }
    }

    //public async Task<bool> AddProductCategoriesAsync(int productID, List<int> categoriesID)
    //{
    //    try
    //    {
    //        var productCategories = categoriesID.Select(categoryID => new ProductCategory
    //        {
    //            ProductID = productID,
    //            CategoryID = categoryID
    //        }).ToList();

    //        await _dbContext.ProductCategory.AddRangeAsync(productCategories);
    //        await _dbContext.SaveChangesAsync();
    //        return true;
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.WriteLine("Error: " + ex.Message);
    //        return false;
    //    }
    //}

    //public async Task<bool> AddToStockAsync(int productID, decimal quantity, decimal threshold)
    //{
    //    try
    //    {
    //        var stock = new Stock
    //        {
    //            ProductID = productID,
    //            Quantity = quantity,
    //            Threshold = threshold
    //        };
    //        await _dbContext.Stock.AddAsync(stock);
    //        await _dbContext.SaveChangesAsync();
    //        return true;
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.WriteLine("Error: " + ex.Message);
    //        return false;
    //    }
    //}
}