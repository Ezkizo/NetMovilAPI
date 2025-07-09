namespace NetMovilAPI.Domain.Entities.User;
public class UserEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public int UserStatusID { get; set; }
    public string UserName { get; set; } = null!;
    public string NormalizedUserName { get; set; } = null!;
    public string NormalizedEmail { get; set; } = null!;
    public bool EmailConfirmed { get; set; }
    public string PasswordHash { get; set; } = null!;
    public string SecurityStamp { get; set; } = null!;
    public string ConcurrencyStamp { get; set; } = null!;
    public bool PhoneNumberConfirmed { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public int AccessFailedCount { get; set; }
    public string? ProfileImageUrl { get; set; } = "defaultprofileimage.png";
    public string? EmergencyContactPhone { get; set; }
    public string? EmergencyContactName { get; set; }
}
