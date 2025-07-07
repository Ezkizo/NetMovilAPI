using NetMovilAPI.Domain.Entities.BaseEntities;
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
    public async Task<ApiResponse<TOutput>> ExecuteAsync(int id, int idUser)
    {
        var data = await _repository.DeleteAsync(id, idUser);
        var result = _presenter.Present(data.Data!);
        return new ApiResponse<TOutput>
        {
            Data = result,
            Message = data.Message,
            Success = data.Success,
            Errors = data.Errors
        };
    }
}