using NetMovilAPI.Domain.Entities.BaseEntities;
using NetMovilAPI.Domain.Interfaces;

namespace NetMovilAPI.Application.UseCases.CategoryUseCases;
public class DeleteCategoryUseCase<TEntity, TOutput>
{
    private readonly IActionRepository<TEntity> _repository;
    private readonly IPresenter<TEntity, TOutput> _presenter;
    public DeleteCategoryUseCase(IActionRepository<TEntity> categoryRepository, IPresenter<TEntity, TOutput> presenter)
    {
        _repository = categoryRepository;
        _presenter = presenter;
    }
    public async Task<ApiResponse<TOutput>> ExecuteAsync(int id, int idUser)
    {
        var response = await _repository.DeleteAsync(id, idUser);
        var result = _presenter.Present(response.Data!);
        return new ApiResponse<TOutput>
        {
            Data = result,
            Message = response.Message,
            Success = response.Success,
            Errors = response.Errors
        };
    }
}

