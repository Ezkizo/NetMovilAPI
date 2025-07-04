using NetMovilAPI.Application.Presenters.ViewModels;
using NetMovilAPI.Domain.Entities.Order;
using NetMovilAPI.Domain.Interfaces;

namespace NetMovilAPI.Application.Presenters;

public class OrderPresenter : IPresenter<OrderEntity, OrderViewModel>
{
    public OrderViewModel Present(OrderEntity data)
    {
        return new OrderViewModel
        {
            OrderID = data.OrderID,
            EmployeeID = data.EmployeeID,
            CustomerID = data.CustomerID,
            CustomerName = data.CustomerName,
            TotalAmount = data.TotalAmount,
            BarCode = data.BarCode ?? "No code",
            Notes = data.Notes ?? "Sin notas adicionales",
            OrderStatusID = data.OrderStatusID,
            OrderCount = data.OrderCount,
            OrderProducts = data.OrderProducts?
            .Select(opm => new OrderProductViewModel
            {
                OrderProductID = opm.OrderProductID,
                OrderID = opm.OrderID,
                Quantity = opm.Quantity,
                ProductID = opm.ProductID,
                ProductName = opm.Product?.Name ?? string.Empty
            })
            .ToList()
            ?? null,
            OrderDate = data.CreatedAt,
            CreatedBy = data.CreatedBy,
        };
    }
    public IEnumerable<OrderViewModel> Present(IEnumerable<OrderEntity> data) => [.. data.Select(Present)];
}