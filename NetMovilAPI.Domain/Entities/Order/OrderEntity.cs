using NetMovilAPI.Domain.Entities.BaseEntities;

namespace NetMovilAPI.Domain.Entities.Order;
public class OrderEntity : AuditableEntity
{
    public int OrderID { get; set; }
    public int EmployeeID { get; set; }
    public decimal TotalAmount { get; set; }
    public int OrderCount { get => OrderProducts?.Sum(op => op.Quantity) ?? 0; }
    public string? BarCode { get; set; }
    public string? Notes { get; set; } = "Sin indicaciones adicionales";
    public int OrderStatusID { get; set; }
    public StatusEntity? OrderStatus { get; set; }
    public int? CustomerID { get; set; }
    public string? CustomerName { get; set; }
    public List<OrderProductEntity>? OrderProducts { get; set; }
    public List<string>? Errors { get; set; } = new();
    public int BranchID { get; set; } // NUEVO CAMPO
}
