using NetMovilAPI.Application.DTOs.Requests;
using NetMovilAPI.Domain.Entities.Order;
using NetMovilAPI.Domain.Interfaces;

namespace NetMovilAPI.Application.Mappers;

public class OrderMapper : IMapper<OrderRequestDTO, OrderEntity>
{
    public OrderEntity ToEntity(OrderRequestDTO dto)
    {
        return new OrderEntity
        {
            OrderID = dto.OrderID,
            EmployeeID = dto.EmployeeID,
            BarCode = dto.BarCode,
            Notes = dto.Notes,
            TotalAmount = dto.TotalAmount,
            CustomerID = dto.CustomerID == 0 ? null : dto.CustomerID,
            CustomerName = dto.CustomerName,
            OrderProducts = dto.OrderProducts?.Select(op => new OrderProductEntity
            {
                OrderID = op.OrderID,
                ProductID = op.ProductID,
                Quantity = op.Quantity
            }).ToList()
            ?? null,
            OrderStatusID = dto.OrderStatusID,
            CreatedBy = dto.CreatedBy,
            BranchID = dto.BranchID
        };
    }

    public IEnumerable<OrderEntity> ToEntity(List<OrderRequestDTO> dtos)
    {
        throw new NotImplementedException();
    }
}
