namespace NetMovilAPI.Domain.Interfaces;
public interface IPresenter<TInput, TOutput>
{
    public TOutput Present(TInput data);
    public IEnumerable<TOutput> Present(IEnumerable<TInput> data);
}

