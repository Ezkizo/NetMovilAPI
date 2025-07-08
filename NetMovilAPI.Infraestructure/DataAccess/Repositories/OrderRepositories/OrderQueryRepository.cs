using Microsoft.EntityFrameworkCore;
using NetMovilAPI.Domain.Entities.Order;
using NetMovilAPI.Domain.Entities.Product;
using NetMovilAPI.Domain.Interfaces;
using NetMovilAPI.Infraestructure.Models.OrderModels;
using System.Linq.Expressions;

namespace NetMovilAPI.Infraestructure.DataAccess.Repositories.OrderRepositories;
public class OrderQueryRepository : IQueryRepository<Order, OrderEntity>
{
    private readonly AppDbContext _dbContext;
    public OrderQueryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OrderEntity> GetQueryAsync(Expression<Func<Order, bool>> filter)
    {
        var data = await _dbContext.Order
            .AsNoTracking()
            .Where(filter)
            .Select(o => new OrderEntity
            {
                OrderID = o.OrderID,
                EmployeeID = o.EmployeeID,
                TotalAmount = o.TotalAmount,
                BarCode = o.BarCode,
                Notes = o.Notes,
                CustomerID = o.CustomerID,
                OrderStatusID = o.OrderStatusID,
                CustomerName = o.CustomerID != null && o.Customer != null && o.Customer.User != null
                    ? o.Customer.User.FirstName + " " + o.Customer.User.LastName
                    : o.CustomerName,
                OrderProducts = o.OrderProducts.Select(op => new OrderProductEntity
                {
                    OrderProductID = op.OrderProductID,
                    ProductID = op.ProductID,
                    OrderID = o.OrderID,
                    Product = op.Product != null 
                    ? new ProductEntity
                    {
                        Name = op.Product.Name,
                        UnitPrice = op.Product.UnitPrice
                    }
                    : null
                }).ToList(),
                CreatedAt = o.CreatedAt,
                CreatedBy = o.CreatedBy
            })
            .FirstOrDefaultAsync();
        return data ?? new OrderEntity { OrderID = 0, Notes = "No fue posible encontrar la orden solicitada, verifique el Id proporcionado" };
    }

    public async Task<IEnumerable<OrderEntity>> GetQueryEnumerableAsync(Expression<Func<Order, bool>> filter)
    {
        return await _dbContext.Order
            .AsNoTracking()
            .Where(filter)
            .Select(o => new OrderEntity
            {
                OrderID = o.OrderID,
                UserID = o.UserID,
                TotalAmount = o.TotalAmount,
                BarCode = o.BarCode,
                Notes = o.Notes,
                CustomerID = o.CustomerID,
                OrderStatusID = o.OrderStatusID,
                CustomerName = o.CustomerID != null && o.Customer != null && o.Customer.User != null
                    ? o.Customer.User.FirstName + " " + o.Customer.User.LastName
                    : o.CustomerName,
                OrderProducts = o.OrderProducts.Select(op => new OrderProductEntity
                {
                    OrderProductID = op.OrderProductID,
                    OrderID = o.OrderID,
                    ProductID = op.ProductID,
                    Product = op.Product != null
                    ? new ProductEntity
                    {
                        Name = op.Product.Name,
                        UnitPrice = op.Product.UnitPrice,
                    }
                    : null
                }).ToList(),
                CreatedAt = o.CreatedAt,
                CreatedBy = o.CreatedBy
            })
            .ToListAsync();
    }
}
