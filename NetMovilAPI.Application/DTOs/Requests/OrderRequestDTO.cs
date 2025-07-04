namespace NetMovilAPI.Application.DTOs.Requests;
public class OrderRequestDTO
{
    public int OrderID { get; set; }
    public int EmployeeID { get; set; }
    public decimal TotalAmount { get; set; }
    public string? BarCode { get; set; }
    public string? Notes { get; set; } = "Sin indicaciones adicionales";
    public int OrderStatusID { get; set; }
    public int? CustomerID { get; set; }
    public string? CustomerName { get; set; }
    public int? TableID { get; set; }
    public int CreatedBy { get; set; }
    public List<OrderProductRequestDTO>? OrderProducts { get; set; }
    public int BranchID { get; set; } // NUEVO CAMPO
}

public class OrderProductRequestDTO
{
    public int OrderProductID { get; set; }
    public int Quantity { get; set; }
    public int OrderID { get; set; }
    public int ProductID { get; set; }
}

