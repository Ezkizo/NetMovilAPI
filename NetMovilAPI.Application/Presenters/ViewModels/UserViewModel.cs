namespace NetMovilAPI.Application.Presenters.ViewModels;

public class UserViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public int UserStatusID { get; set; }
    public string? UserStatus { get; set; }
    public string UserName { get; set; } = null!;
    public bool EmailConfirmed { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public int AccessFailedCount { get; set; }
    public int? BranchID { get; set; }
    public string? BranchName { get; set; }
}