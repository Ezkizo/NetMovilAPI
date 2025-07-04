using NetMovilAPI.Domain.Interfaces;

namespace NetMovilAPI.Application.UseCases.OrderUseCases;

public class DeleteOrderUseCase<TEntity, TOutput>
{
    private readonly IActionRepository<TEntity> _repository;
    private readonly IPresenter<TEntity, TOutput> _presenter;
    public DeleteOrderUseCase(IActionRepository<TEntity> categoryRepository, IPresenter<TEntity, TOutput> presenter)
    {
        _repository = categoryRepository;
        _presenter = presenter;
    }
    public async Task<TOutput> ExecuteAsync(int id)
    {
        var data = await _repository.DeleteAsync(id);
        return _presenter.Present(data);
    }
}

