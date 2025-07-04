using NetMovilAPI.Domain.Interfaces;

namespace NetMovilAPI.Application.UseCases.OrderUseCases;
public class PostOrderUseCase<TDTO, TEntity, TOutput>
{
    private readonly IActionRepository<TEntity> _repository;
    private readonly IMapper<TDTO, TEntity> _mapper;
    private readonly IPresenter<TEntity, TOutput> _presenter;
    public PostOrderUseCase(IActionRepository<TEntity> repository, IPresenter<TEntity, TOutput> presenter, IMapper<TDTO, TEntity> mapper)
    {
        _repository = repository;
        _presenter = presenter;
        _mapper = mapper;
    }
    public async Task<TOutput> ExecuteAsync(TDTO request)
    {
        var category = _mapper.ToEntity(request);
        await _repository.AddAsync(category);
        return _presenter.Present(category);
    }
}
