using Microsoft.EntityFrameworkCore;
using NetMovilAPI.Domain.Entities.BaseEntities;
using NetMovilAPI.Domain.Entities.Shared;
using NetMovilAPI.Domain.Interfaces;
using NetMovilAPI.Infraestructure.Models.Shared;

namespace NetMovilAPI.Infraestructure.DataAccess.Repositories.CategoryRepositories;
public class CategoryActionRepository : IActionRepository<CategoryEntity>
{
    private readonly AppDbContext _dbContext;
    public CategoryActionRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CategoryEntity> AddAsync(CategoryEntity entity)
    {
        var model = new Category
        {
            CategoryID = entity.CategoryID,
            Name = entity.Name,
            Description = entity.Description,
            ImageUrl = entity.ImageUrl,
            CategoryStatusID = entity.CategoryStatus.Id,
            CreatedAt = DateTimeOffset.UtcNow,
            CreatedBy = entity.CreatedBy,
        };
        try
        {
            await _dbContext.Category.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            entity.CreatedAt = model.CreatedAt;
            entity.CategoryID = model.CategoryID;
            return entity;
        }
        catch (Exception ex)
        {
            entity.CategoryID = 0;
            entity.Description = ex.Message;
            return entity;
        }
    }

    public async Task<CategoryEntity> UpdateAsync(CategoryEntity entity)
    {
        try
        {
            var model = await _dbContext.Category.FirstOrDefaultAsync(c => c.CategoryID == entity.CategoryID);
            if (model == null)
            {
                return new CategoryEntity
                {
                    CategoryID = 0,
                    Description = "No se encontró la categoría"
                };
            }

            model.Name = entity.Name;
            model.Description = entity.Description;
            model.ImageUrl = entity.ImageUrl;
            model.CategoryStatusID = entity.CategoryStatus.Id;
            model.UpdatedBy = entity.CreatedBy;
            model.UpdatedAt = DateTimeOffset.UtcNow;

            await _dbContext.SaveChangesAsync();
            entity.UpdatedAt = model.UpdatedAt;
            return entity;
        }
        catch (Exception ex)
        {
            return new CategoryEntity
            {
                CategoryID = 0,
                Description = ex.Message
            };
        }
    }

    public async Task<ApiResponse<CategoryEntity>> DeleteAsync(int id, int idUser)
    {
        try
        {
            var query = _dbContext.Category.Where(c => c.CategoryID == id);
            var model = await query.FirstOrDefaultAsync();
            if (model == null)
            {
                return new ApiResponse<CategoryEntity>
                {
                    Success = false,
                    Message = "Categoría no encontrada"
                };
            }
            await query.ExecuteUpdateAsync(c => c.SetProperty(ct => ct.CategoryStatusID, 1)
                .SetProperty(ct => ct.UpdatedBy, idUser)
                .SetProperty(ct => ct.UpdatedAt, DateTimeOffset.UtcNow));
            return new ApiResponse<CategoryEntity>
            {
                Success = true,
                Message = "Categoría eliminada correctamente"
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<CategoryEntity>
            {
                Data = new(),
                Success = false,
                Message = "Error al eliminar la categoría: " + ex.Message,
            };
        }
    }
}
