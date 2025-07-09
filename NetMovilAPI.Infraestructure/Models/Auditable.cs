namespace NetMovilAPI.Infraestructure.Models;

public class Auditable
{
    public int CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public int? UpdatedBy { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public int? DeletedBy { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}

