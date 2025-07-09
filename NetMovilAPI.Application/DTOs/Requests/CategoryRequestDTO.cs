namespace NetMovilAPI.Application.DTOs.Requests;

public class CategoryRequestDTO
{
    public int CategoryID { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string ImageUrl { get; set; } = "/iconos/defaultcategoryicon.webp";
    public int CategoryStatusID { get; set; }
    public int CreatedBy { get; set; }
}
