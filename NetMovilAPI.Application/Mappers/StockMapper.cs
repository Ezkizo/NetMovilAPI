using NetMovilAPI.Application.DTOs.Requests;
using NetMovilAPI.Domain.Entities.Shared;
using NetMovilAPI.Domain.Interfaces;

namespace NetMovilAPI.Application.Mappers;

public class StockMapper : IMapper<StockRequestDTO, StockEntity>
{
    public StockEntity ToEntity(StockRequestDTO dto)
    {
        return new StockEntity
        {
            StockID = dto.StockID,
            Quantity = dto.Quantity,
            Threshold = dto.Threshold,
            BranchID = dto.BranchID
        };
    }
    public IEnumerable<StockEntity> ToEntity(List<StockRequestDTO> dtos) => [.. dtos.Select(ToEntity)];
}