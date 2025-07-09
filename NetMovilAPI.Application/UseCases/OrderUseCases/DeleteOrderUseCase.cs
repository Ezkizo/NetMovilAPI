using NetMovilAPI.Domain.Entities.BaseEntities;
using NetMovilAPI.Domain.Interfaces;

namespace NetMovilAPI.Application.UseCases.OrderUseCases;

public class DeleteOrderUseCase<TEntity, TOutput>
{
    private readonly IActionRepository<TEntity> _repository;
    public DeleteOrderUseCase(IActionRepository<TEntity> categoryRepository)
    {
        _repository = categoryRepository;
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

