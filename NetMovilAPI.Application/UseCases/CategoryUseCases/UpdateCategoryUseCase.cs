using NetMovilAPI.Domain.Interfaces;

namespace NetMovilAPI.Application.UseCases.CategoryUseCases;

public class UpdateCategoryUseCase<TDTO, TEntity, TOutput>
{
    private readonly IPresenter<TEntity, TOutput> _presenter;
    private readonly IActionRepository<TEntity> _repository;
    private readonly IMapper<TDTO, TEntity> _mapper;
    public UpdateCategoryUseCase(IPresenter<TEntity, TOutput> presenter, IActionRepository<TEntity> repository, IMapper<TDTO, TEntity> mapper)
    {
        _presenter = presenter;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TOutput> ExecuteAsync(TDTO DTO)
    {
        var result = await _repository.UpdateAsync(_mapper.ToEntity(DTO));
        return _presenter.Present(result);
    }

    //public Task<TOutput> ExecuteAsync(string[] properties, string[] values, Dictionary<string, string> values)
    //{
    //    ; ; ; ; ; ; ; ; ; ;
    //}

}