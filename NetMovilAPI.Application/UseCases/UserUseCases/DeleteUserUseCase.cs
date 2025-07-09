using NetMovilAPI.Domain.Entities.BaseEntities;
using NetMovilAPI.Domain.Interfaces;

namespace NetMovilAPI.Application.UseCases.UserUseCases;

public class DeleteUserUseCase<TEntity, TOutput>
{
    private readonly IActionRepository<TEntity> _repository;
    public DeleteUserUseCase(IActionRepository<TEntity> repository)
    {
        _repository = repository;
    }
    public async Task<ApiResponse<TOutput>> ExecuteAsync(int id, int idUser)
    {
        var data = await _repository.DeleteAsync(id, idUser);
        return new ApiResponse<TOutput>
        {
            Message = data.Message,
            Success = data.Success,
            Errors = data.Errors
        };
    }
}