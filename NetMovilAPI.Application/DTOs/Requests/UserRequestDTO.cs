namespace NetMovilAPI.Application.DTOs.Requests;

public class UserRequestDTO
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public int UserStatusID { get; set; }
    public string? UserName { get; set; }
    public string? PasswordHash { get; set; }
    public string? NewPassword { get; set; } // NUEVO CAMPO
    public int? BranchID { get; set; } // NUEVO CAMPO
}
