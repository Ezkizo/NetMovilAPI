using NetMovilAPI.Application.DTOs.Requests;
using NetMovilAPI.Domain.Entities.Product;
using NetMovilAPI.Domain.Interfaces;

namespace NetMovilAPI.Application.Mappers;
public class ProductMapper : IMapper<ProductRequestDTO, ProductEntity>
{
    public ProductEntity ToEntity(ProductRequestDTO dto)
    {
        return new ProductEntity
        {
            ProductID = dto.ProductID,
            Name = dto.Name ?? "Desconocido",
            Description = dto.Description,
            BasePrice = dto.BasePrice,
            ProfitMargin = dto.ProfitMargin,
            UnitPrice = dto.UnitPrice,
            ImageUrl = dto.ImageUrl,
            BarCode = dto.BarCode,
            IsStock = dto.IsStock,
            BranchID = dto.BranchID,
            ProductCategories = dto?.ProductCategories?.Select(pe => new Domain.Entities.Shared.CategoryEntity
            {
                CategoryID = pe
            }).ToList() ?? [],
            ProductStatus = new Domain.Entities.BaseEntities.StatusEntity { Id = dto?.ProductStatusID ?? 0 }
        };
    }

    public IEnumerable<ProductEntity> ToEntity(List<ProductRequestDTO> dtos) => [.. dtos.Select(ToEntity)];
}

