using Microsoft.EntityFrameworkCore;
using NetMovilAPI.Domain.Entities.BaseEntities;
using NetMovilAPI.Domain.Entities.Order;
using NetMovilAPI.Domain.Interfaces;
using NetMovilAPI.Infraestructure.Models.OrderModels;

namespace NetMovilAPI.Infraestructure.DataAccess.Repositories.OrderRepositories;
public class OrderActionRepository : IActionRepository<OrderEntity>
{
    private readonly AppDbContext _dbContext;
    public OrderActionRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OrderEntity> AddAsync(OrderEntity entity)
    {
        var model = new Order
        {
            EmployeeID = entity.EmployeeID,
            TotalAmount = entity.TotalAmount,
            BarCode = entity.BarCode,
            Notes = entity.Notes,
            OrderStatusID = entity.OrderStatusID,
            CustomerID = entity.CustomerID,
            CustomerName = entity.CustomerName,
            BranchID = entity.BranchID,
            CreatedAt = DateTime.Now,
            CreatedBy = entity.CreatedBy,
            OrderProducts = entity.OrderProducts?
                .Select(op => new OrderProduct
                {
                    ProductID = op.ProductID,
                    Quantity = op.Quantity
                })
                .ToList() ?? []
        };

        try
        {
            _dbContext.Order.Add(model);
            await _dbContext.SaveChangesAsync();

            entity.OrderID = model.OrderID;
            entity.CreatedAt = model.CreatedAt;
            return entity;
        }
        catch (Exception ex)
        {
            return new OrderEntity { OrderID = 0, Notes = $"Ha ocurrido un error, contacte al administrador. \n Error: {ex.Message}" };
        }
    }

    public async Task<OrderEntity> UpdateAsync(OrderEntity entity)
    {
        // 1. Cargar la orden existente con detalles
        var order = await _dbContext.Order
            .Include(o => o.OrderProducts)
            .FirstOrDefaultAsync(o => o.OrderID == entity.OrderID);

        if (order == null)
            return new OrderEntity { OrderID = 0, Notes = "Orden no encontrada" };

        // 2. Actualizar campos de cabecera
        order.Notes = entity.Notes;
        order.TotalAmount = entity.TotalAmount;
        order.BarCode = entity.BarCode;
        order.OrderStatusID = entity.OrderStatusID;
        order.CustomerID = entity.CustomerID;
        order.CustomerName = entity.CustomerName;

        // 3. Sincronizar colección OrderProducts
        var incomingIds = entity.OrderProducts?.Where(x => x.OrderProductID != 0)
                .Select(x => x.OrderProductID)
                .ToHashSet();

        // 3.1 Eliminar los que no vienen
        if (incomingIds != null)
        {
            var toRemove = order.OrderProducts
                .Where(op => !incomingIds.Contains(op.OrderProductID))
                .ToList();
            _dbContext.OrderProduct.RemoveRange(toRemove);
        }

        // 3.2 Insertar nuevos y actualizar existentes
        // Validación de si hay productos entrantes
        if (entity.OrderProducts != null && entity.OrderProducts.Any())
        {
            foreach (var incoming in entity.OrderProducts)
            {
                if (incoming.OrderProductID == 0)
                {
                    // Nuevo detalle
                    order.OrderProducts.Add(new OrderProduct
                    {
                        ProductID = incoming.ProductID,
                        Quantity = incoming.Quantity
                    });
                }
                else
                {
                    // Actualizar existente
                    var existing = order.OrderProducts
                        .First(op => op.OrderProductID == incoming.OrderProductID);
                    existing.Quantity = incoming.Quantity;
                }
            }
        }

        // 4. Guardar cambios
        try
        {
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        catch (Exception)
        {
            return new OrderEntity { OrderID = entity.OrderID, Notes = "Error al actualizar la orden" };
        }
    }

    public async Task<ApiResponse<OrderEntity>> DeleteAsync(int id, int idUser)
    {
        try
        {
            var query = _dbContext.Category.Where(c => c.CategoryID == id);
            await query.ExecuteUpdateAsync(c => c.SetProperty(ct => ct.CategoryStatusID, 1));
            await query.ExecuteUpdateAsync(c => c.SetProperty(ct => ct.DeletedAt, DateTime.Now));
            await query.ExecuteUpdateAsync(c => c.SetProperty(ct => ct.DeletedBy, idUser));

            return new ApiResponse<OrderEntity>
            {
                Success = true,
                Message = "Orden eliminada correctamente",
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<OrderEntity>
            {
                Success = true,
                Message = "Ocurrio un error al eliminar la orden: " + ex.Message,
            };
        }
    }
}

