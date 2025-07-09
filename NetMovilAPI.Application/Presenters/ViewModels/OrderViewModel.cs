using System.Text.Json.Serialization;

namespace NetMovilAPI.Application.Presenters.ViewModels;

public class OrderViewModel
{
    public int OrderID { get; set; }
    public int EmployeeID { get; set; }
    public decimal TotalAmount { get; set; }
    public string BarCode { get; set; } = null!;
    public int OrderCount { get; set; }
    public string Notes { get; set; } = "Sin indicaciones adicionales";
    public int OrderStatusID { get; set; }
    public int? CustomerID { get; set; }
    public string? CustomerName { get; set; }
    public int? TableID { get; set; }
    public List<OrderProductViewModel>? OrderProducts { get; set; }
    public DateTimeOffset? OrderDate { get; set; }
    public int CreatedBy { get; set; }
    public int BranchID { get; set; } // NUEVO CAMPO
    [JsonIgnore]
    public List<string>? Errors { get; set; } = [];
}

public class OrderProductViewModel
{
    public int OrderProductID { get; set; }
    public int Quantity { get; set; }
    public int OrderID { get; set; }
    public int ProductID { get; set; }
    public string? ProductName { get; set; }
}
