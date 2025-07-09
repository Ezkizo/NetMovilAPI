using NetMovilAPI.Domain.Entities.BaseEntities;
using NetMovilAPI.Domain.Interfaces;

namespace NetMovilAPI.Application.UseCases.EmployeeUseCases;

public class DeleteEmployeeUseCase<TEntity, TOutput>
{
    private readonly IActionRepository<TEntity> _repository;
    public DeleteEmployeeUseCase(IActionRepository<TEntity> repository)
    {
        _repository = repository;
    }
    public async Task<ApiResponse<TOutput>> ExecuteAsync(int id, int idUser)
    {
        var response = await _repository.DeleteAsync(id, idUser);
        return new ApiResponse<TOutput>
        {
            Message = response.Message,
            Success = response.Success,
            Errors = response.Errors
        };
    }
}