namespace NetMovilAPI.Domain.Interfaces;
public interface IMapper<TDTO, TOutput>
{
    public TOutput ToEntity(TDTO dto);
    public IEnumerable<TOutput> ToEntity(List<TDTO> dtos);
}
