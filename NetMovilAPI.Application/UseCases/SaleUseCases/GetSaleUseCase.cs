using NetMovilAPI.Domain.Interfaces;
using System.Linq.Expressions;

namespace NetMovilAPI.Application.UseCases.SaleUseCases;
public class GetSaleUseCase<TQuery, TEntity, TOutput>
{
    private readonly IQueryRepository<TQuery, TEntity> _repository;
    private readonly IPresenter<TEntity, TOutput> _presenter;
    public GetSaleUseCase(IQueryRepository<TQuery, TEntity> repository, IPresenter<TEntity, TOutput> presenter)
    {
        _presenter = presenter;
        _repository = repository;
    }

    public async Task<IEnumerable<TOutput>> ExecuteAsync(Expression<Func<TQuery, bool>> filter)
    {
        var response = await _repository.GetQueryEnumerableAsync(filter);
        return _presenter.Present(response);
    }

}
