using NetMovilAPI.Domain.Interfaces;

namespace NetMovilAPI.Application.UseCases.EmployeeUseCases;

public class DeleteEmployeeUseCase<TEntity, TOutput>
{
    private readonly IActionRepository<TEntity> _repository;
    private readonly IPresenter<TEntity, TOutput> _presenter;
    public DeleteEmployeeUseCase(IActionRepository<TEntity> repository, IPresenter<TEntity, TOutput> presenter)
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