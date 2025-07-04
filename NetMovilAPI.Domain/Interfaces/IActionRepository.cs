namespace NetMovilAPI.Domain.Interfaces;
public interface IActionRepository<TEntity>
{
    public Task<TEntity> AddAsync(TEntity entity);
    public Task<TEntity> UpdateAsync(TEntity entity);
    public Task<TEntity> DeleteAsync(int id);
}

