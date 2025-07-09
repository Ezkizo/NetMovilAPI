using NetMovilAPI.Domain.Interfaces;
using System.Linq.Expressions;

namespace NetMovilAPI.Application.UseCases.EmployeeUseCases;

public class GetEmployeeUseCase<TQuery, TEntity, TOutput>
{
    private readonly IQueryRepository<TQuery, TEntity> _repository;
    private readonly IPresenter<TEntity, TOutput> _presenter;
    public GetEmployeeUseCase(IQueryRepository<TQuery, TEntity> repository, IPresenter<TEntity, TOutput> presenter)
    {
        _repository = repository;
        _presenter = presenter;
    }
    public async Task<IEnumerable<TOutput>> ExecuteAsync(Expression<Func<TQuery, bool>> filter)
    {
        var data = await _repository.GetQueryEnumerableAsync(filter);
        return _presenter.Present(data);
    }
}