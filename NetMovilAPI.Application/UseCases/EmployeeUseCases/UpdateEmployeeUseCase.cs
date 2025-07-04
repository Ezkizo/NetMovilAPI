using NetMovilAPI.Domain.Interfaces;

namespace NetMovilAPI.Application.UseCases.EmployeeUseCases;

public class UpdateEmployeeUseCase<TDTO, TEntity, TOutput>
{
    private readonly IPresenter<TEntity, TOutput> _presenter;
    private readonly IActionRepository<TEntity> _repository;
    private readonly IMapper<TDTO, TEntity> _mapper;
    public UpdateEmployeeUseCase(IPresenter<TEntity, TOutput> presenter, IActionRepository<TEntity> repository, IMapper<TDTO, TEntity> mapper)
    {
        _presenter = presenter;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TOutput> ExecuteAsync(TDTO dto)
    {
        var result = await _repository.UpdateAsync(_mapper.ToEntity(dto));
        return _presenter.Present(result);
    }
}