using System.Linq.Expressions;

namespace NetMovilAPI.Domain.Interfaces;
public interface IQueryRepository<TModelQuery, TEntity>
{
    public Task<TEntity> GetQueryAsync(Expression<Func<TModelQuery, bool>> filter);
    public Task<IEnumerable<TEntity>> GetQueryEnumerableAsync(Expression<Func<TModelQuery, bool>> filter);
}

