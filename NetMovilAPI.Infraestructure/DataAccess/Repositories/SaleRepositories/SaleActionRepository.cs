using Microsoft.EntityFrameworkCore;
using NetMovilAPI.Domain.Entities.BaseEntities;
using NetMovilAPI.Domain.Entities.Sale;
using NetMovilAPI.Domain.Interfaces;
using NetMovilAPI.Infraestructure.Models.SaleModels;
using System.Diagnostics;

namespace NetMovilAPI.Infraestructure.DataAccess.Repositories.SaleRepositories;
public class SaleActionRepository : IActionRepository<SaleEntity>
{
    private readonly AppDbContext _dbContext;

    public SaleActionRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SaleEntity> AddAsync(SaleEntity entity)
    {
        // 1. Mapear SaleEntity a Sale (modelo EF)
        var sale = new Sale
        {
            TotalPaid = entity.TotalPaid,
            OrderID = entity.Order.OrderID,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = entity.CreatedBy,
            Payments = entity.Payments?.Select(p => new SalePayment
            {
                Amount = p.Amount,
                Reference = p.Reference,
                PaymentMethodID = p.PaymentMethodID,
                PaymentStatusID = p.PaymentStatusID,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = entity.CreatedBy
            }).ToList() ?? []
        };

        try
        {
            await _dbContext.Sale.AddAsync(sale);
            await _dbContext.SaveChangesAsync();

            // Mapear de vuelta a SaleEntity
            entity.SaleID = sale.SaleID;
            entity.CreatedAt = sale.CreatedAt;
            entity.Payments = sale.Payments.Select(sp => new SalePaymentEntity
            {
                SalePaymentID = sp.SalePaymentID,
                Amount = sp.Amount,
                Reference = sp.Reference,
                SaleID = sp.SaleID,
                PaymentMethodID = sp.PaymentMethodID,
                PaymentMehod = sp.PaymentMethod.Description ?? "Desconocido",
                PaymentStatusID = sp.PaymentStatusID,
                PaymentStatus = sp.PaymentStatus.Description,
            }).ToList();

            return entity;
        }
        catch (DbUpdateException dbEx)
        {
            Debug.WriteLine($"Error en BD: {dbEx.Message}");
            entity.SaleID = 0;
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error genérico: {ex.Message}");
            entity.SaleID = 0;
            return entity;
        }
    }

    public async Task<SaleEntity> UpdateAsync(SaleEntity entity)
    {
        try
        {
            var sale = await _dbContext.Sale
                .Include(s => s.Payments)
                .FirstOrDefaultAsync(s => s.SaleID == entity.SaleID);

            if (sale == null)
            {
                return entity;
            }

            sale.TotalPaid = entity.TotalPaid;
            sale.UpdatedAt = DateTime.UtcNow;
            sale.UpdatedBy = entity.UpdatedBy;

            // Actualizar Payments (simple: eliminar y volver a agregar)
            if (entity.Payments != null)
            {
                _dbContext.SalePayment.RemoveRange(sale.Payments);
                sale.Payments = entity.Payments.Select(p => new SalePayment
                {
                    Amount = p.Amount,
                    Reference = p.Reference,
                    PaymentMethodID = p.PaymentMethodID,
                    PaymentStatusID = p.PaymentStatusID,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = entity.UpdatedBy ?? entity.CreatedBy
                }).ToList();
            }

            await _dbContext.SaveChangesAsync();

            entity.UpdatedAt = sale.UpdatedAt;
            entity.Payments = sale.Payments.Select(sp => new SalePaymentEntity
            {
                SalePaymentID = sp.SalePaymentID,
                Amount = sp.Amount,
                Reference = sp.Reference,
                SaleID = sp.SaleID,
                PaymentMethodID = sp.PaymentMethodID,
                PaymentMehod = sp.PaymentMethod.Description ?? "Desconocido",
                PaymentStatusID = sp.PaymentStatusID,
                PaymentStatus = sp.PaymentStatus.Description,
            }).ToList();

            return entity;
        }
        catch (DbUpdateException dbEx)
        {
            Debug.WriteLine($"Error en BD: {dbEx.Message}");
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error genérico: {ex.Message}");
            return entity;
        }
    }

    public async Task<ApiResponse<SaleEntity>> DeleteAsync(int id, int idUser)
    {
        try
        {
            var sale = await _dbContext.Sale.FirstOrDefaultAsync(s => s.SaleID == id);
            if (sale != null)
            {
                sale.SaleStatusID = 1; // Suponiendo 1 = eliminado/cancelado
                sale.DeletedAt = DateTime.UtcNow;
                sale.DeletedBy = idUser;
                await _dbContext.SaveChangesAsync();

                return new ApiResponse<SaleEntity>
                {
                    Success = true,
                    Message = "Venta eliminada con éxito."
                };
            }
            else
            {
                return new ApiResponse<SaleEntity>
                {
                    Success = false,
                    Message = "No se encontró la venta."
                };
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error: {ex.Message}");
            return new ApiResponse<SaleEntity>
            {
                Success = false,
                Message = "Ocurrió un error al eliminar la venta, si el problema persiste contacte al administrador.",
                Errors = [ex.Message]
            };
        }
    }
}
