using NetMovilAPI.Application.Presenters.ViewModels;
using NetMovilAPI.Domain.Entities.Shared;
using NetMovilAPI.Domain.Interfaces;

namespace NetMovilAPI.Application.Presenters;

public class CategoryPresenter : IPresenter<CategoryEntity, CategoryViewModel>
{
    public CategoryViewModel Present(CategoryEntity data)
    {
        return new CategoryViewModel
        {
            CategoryID = data.CategoryID,
            Name = data.Name,
            Description = data.Description,
            ImageUrl = data.ImageUrl,
            CategoryStatusID = data.CategoryStatus is null
                                                        ? 0
                                                        : data.CategoryStatus.Id,
            CategoryStatus = data.CategoryStatus is null
                                                        ? "Error"
                                                        : data.CategoryStatus.Description,
            CreatedAt = data.CreatedAt,
            CreatedBy = data.CreatedBy
        };
    }
    public IEnumerable<CategoryViewModel> Present(IEnumerable<CategoryEntity> data) => [.. data.Select(Present)];
}
