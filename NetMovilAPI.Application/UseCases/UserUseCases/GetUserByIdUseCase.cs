using NetMovilAPI.Domain.Interfaces;
using System.Linq.Expressions;

namespace NetMovilAPI.Application.UseCases.UserUseCases;

public class GetUserByIdUseCase<TQuery, TEntity, TOutput>
{
    private readonly IQueryRepository<TQuery, TEntity> _repository;
    private readonly IPresenter<TEntity, TOutput> _presenter;
    public GetUserByIdUseCase(IQueryRepository<TQuery, TEntity> repository, IPresenter<TEntity, TOutput> presenter)
    {
        _repository = repository;
        _presenter = presenter;
    }
    public async Task<TOutput> ExecuteAsync(Expression<Func<TQuery, bool>> filter)
    {
        var data = await _repository.GetQueryAsync(filter);
        return _presenter.Present(data);
    }
}