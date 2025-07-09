using NetMovilAPI.Domain.Interfaces;
using System.Linq.Expressions;
namespace NetMovilAPI.Application.UseCases.SaleUseCases;

public class GetSaleByIdUseCase<TQuery, TEntity, TOutput>
{
    private readonly IQueryRepository<TQuery, TEntity> _repository;
    private readonly IPresenter<TEntity, TOutput> _presenter;
    public GetSaleByIdUseCase(IQueryRepository<TQuery, TEntity> repository, IPresenter<TEntity, TOutput> presenter)
    {
        _presenter = presenter;
        _repository = repository;
    }

    public async Task<TOutput> ExecuteAsync(Expression<Func<TQuery, bool>> filter)
    {
        var response = await _repository.GetQueryAsync(filter);
        return _presenter.Present(response);
    }
}

