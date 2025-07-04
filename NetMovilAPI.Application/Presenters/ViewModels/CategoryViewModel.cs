namespace NetMovilAPI.Application.Presenters.ViewModels;

public class CategoryViewModel
{
    public int CategoryID { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int CategoryStatusID { get; set; }
    public string? CategoryStatus { get; set; }
    public string ImageUrl { get; set; } = "/iconos/defaultcategoryicon.png";
    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
}

