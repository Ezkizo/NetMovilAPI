using NetMovilAPI.Domain.Entities.BaseEntities;
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