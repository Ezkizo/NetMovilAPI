using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NetMovilAPI.Infraestructure.Models.Statuses;
using NetMovilAPI.Infraestructure.Models.UserModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetMovilAPI.Infraestructure.Models.OrderModels;

public class Order : Auditable
{
    [Key]
    public int OrderID { get; set; }
    public decimal TotalAmount { get; set; }
    public string? BarCode { get; set; }
    public string? Notes { get; set; } = "Sin indicaciones adicionales";
    public string? CustomerName { get; set; }
    public int BranchID { get; set; } // NUEVO CAMPO

    // Definir las llaves foráneas
    public List<OrderProduct> OrderProducts { get; set; } = new();

    [ForeignKey("OrderStatusID")]
    public int OrderStatusID { get; set; }
    public OrderStatus? OrderStatus { get; set; }

    [ForeignKey("EmployeeID")]
    public int EmployeeID { get; set; }
    public Employee? Employee { get; set; }

    [ForeignKey("AddressID")]
    public int? CustomerAddressID { get; set; }
    public CustomerAddress? CustomerAddress { get; set; }

    [ForeignKey("CustomerID")]
    public int? CustomerID { get; set; }
    public Customer? Customer { get; set; }

}