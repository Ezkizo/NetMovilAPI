namespace NetMovilAPI.Application.DTOs.Requests;
public class CustomerRequestDTO
{
    public int CustomerID { get; set; }
    public int UserID { get; set; }
}

public class CustomerAddressRequestDTO
{
    public int CustomerAddressID { get; set; }
    public int CustomerID { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? DeliveryReferences { get; set; } = "Sin referencias";
    public int? PostalCode { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
}

