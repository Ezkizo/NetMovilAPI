namespace NetMovilAPI.Application.DTOs.Requests;

public class EmployeeRequestDTO
{
    public int EmployeeID { get; set; }
    public string? ProfileImage { get; set; } = "defaultprofilepicture.webp";
    public string? EmergencyContact { get; set; }
    public string? EmergencyContactName { get; set; }
}
