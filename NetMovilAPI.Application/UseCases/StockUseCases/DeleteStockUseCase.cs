using NetMovilAPI.Domain.Interfaces;

namespace NetMovilAPI.Application.UseCases.StockUseCases;

public class DeleteStockUseCase<TEntity, TOutput>
{
    private readonly IActionRepository<TEntity> _repository;
    private readonly IPresenter<TEntity, TOutput> _presenter;
    public DeleteStockUseCase(IActionRepository<TEntity> repository, IPresenter<TEntity, TOutput> presenter)
    {
        _repository = repository;
        _presenter = presenter;
    }
    public async Task<TOutput> ExecuteAsync(int id)
    {
        var data = await _repository.DeleteAsync(id);
        return _presenter.Present(data);
    }
}