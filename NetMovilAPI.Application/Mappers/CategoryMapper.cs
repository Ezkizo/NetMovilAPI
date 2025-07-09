using NetMovilAPI.Application.DTOs.Requests;
using NetMovilAPI.Domain.Entities.Shared;
using NetMovilAPI.Domain.Interfaces;

namespace NetMovilAPI.Application.Mappers;

public class CategoryMapper : IMapper<CategoryRequestDTO, CategoryEntity>
{
    public CategoryEntity ToEntity(CategoryRequestDTO dto)
    {
        return new CategoryEntity
        {
            CategoryID = dto.CategoryID,
            Name = dto.Name ?? "Error al recuperar el nombre",
            Description = dto.Description ?? "Error al recuperar la descripción",
            ImageUrl = dto.ImageUrl,
            CategoryStatus = new Domain.Entities.BaseEntities.StatusEntity { Id = dto.CategoryStatusID },
            CreatedBy = dto.CreatedBy
        };
    }

    public IEnumerable<CategoryEntity> ToEntity(List<CategoryRequestDTO> dtos) => [.. dtos.Select(ToEntity)];
}


