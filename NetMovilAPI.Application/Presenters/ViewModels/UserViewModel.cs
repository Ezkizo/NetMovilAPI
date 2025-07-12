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
    public string? ProfileImageUrl { get; set; } = "defaultprofileimage.png";
    public string? EmergencyContactPhone { get; set; }
    public string? EmergencyContactName { get; set; }
}

public class UserAddressViewModel
{
    public int CustomerAddressID { get; set; }
    public int CustomerID { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? References { get; set; } = "Sin referencias";
    public int? PostalCode { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
}