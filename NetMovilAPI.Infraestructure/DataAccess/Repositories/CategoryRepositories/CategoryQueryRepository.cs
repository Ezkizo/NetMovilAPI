using Microsoft.EntityFrameworkCore;
using NetMovilAPI.Domain.Entities.Shared;
using NetMovilAPI.Domain.Interfaces;
using NetMovilAPI.Infraestructure.Models.Shared;
using System.Linq.Expressions;

namespace NetMovilAPI.Infraestructure.DataAccess.Repositories.CategoryRepositories;

public class CategoryQueryRepository : IQueryRepository<Category, CategoryEntity>
{
    private readonly AppDbContext _dbContext;
    public CategoryQueryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<CategoryEntity> GetQueryAsync(Expression<Func<Category, bool>> filter)
    {
        var data = await _dbContext.Category
            .AsNoTracking()
            .Where(filter)
            .Select(c => new CategoryEntity
            {
                CategoryID = c.CategoryID,
                Name = c.Name,
                Description = c.Description,
                ImageUrl = c.ImageUrl,
                CategoryStatus = new Domain.Entities.BaseEntities.StatusEntity
                {
                    Id = c.CategoryStatus.CategoryStatusID,
                    Description = c.CategoryStatus.Description
                },
                CreatedAt = c.CreatedAt,
                CreatedBy = c.CreatedBy,
                UpdatedAt = c.UpdatedAt,
                UpdatedBy = c.UpdatedBy
            }).FirstOrDefaultAsync();

        return data ??= new CategoryEntity { CategoryID = 0 };
    }

    public async Task<IEnumerable<CategoryEntity>> GetQueryEnumerableAsync(Expression<Func<Category, bool>> filter)
    {
        var data = await _dbContext.Category
            .AsNoTracking()
            .Where(filter)
            .Include(c => c.CategoryStatus).Select(c => new CategoryEntity
            {
                CategoryID = c.CategoryID,
                Name = c.Name,
                Description = c.Description,
                ImageUrl = c.ImageUrl,
                CategoryStatus = new Domain.Entities.BaseEntities.StatusEntity
                {
                    Id = c.CategoryStatus.CategoryStatusID,
                    Description = c.CategoryStatus.Description
                },
                CreatedAt = c.CreatedAt,
                CreatedBy = c.CreatedBy,
                UpdatedAt = c.UpdatedAt,
                UpdatedBy = c.UpdatedBy
            }).ToListAsync();

        return data;
    }
}
