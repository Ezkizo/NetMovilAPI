using Microsoft.EntityFrameworkCore;
using NetMovilAPI.Domain.Entities.Order;
using NetMovilAPI.Domain.Entities.Sale;
using NetMovilAPI.Domain.Interfaces;
using NetMovilAPI.Infraestructure.Models.SaleModels;
using System.Linq.Expressions;

namespace NetMovilAPI.Infraestructure.DataAccess.Repositories.SaleRepositories;
public class SaleQueryRepository : IQueryRepository<Sale, SaleEntity>
{
    private readonly AppDbContext _dbContext;
    public SaleQueryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<SaleEntity> GetQueryAsync(Expression<Func<Sale, bool>> filter)
    {
        var data = await _dbContext.Sale
            .Where(filter)
            .Select(c => new SaleEntity
            {
                SaleID = c.SaleID,
                TotalPaid = c.TotalPaid,
                Order = new OrderEntity
                {
                    OrderID = c.Order.OrderID,
                    TotalAmount = c.Order.TotalAmount,
                    CustomerName = c.Order.CustomerName
                },
                Payments = c.Payments.Select(p => new SalePaymentEntity
                {
                    SalePaymentID = p.SalePaymentID,
                    Amount = p.Amount,
                    PaymentMethodID = p.PaymentMethodID,
                    PaymentStatus = new Domain.Entities.BaseEntities.PaymentStatusEntity
                    {
                        Description = p.PaymentStatus.Description
                    },
                }).ToList(),
                CreatedAt = c.CreatedAt,
                CreatedBy = c.CreatedBy
            })
            .FirstOrDefaultAsync();
        return data ??= new SaleEntity { SaleID = 0 };
    }

    public async Task<IEnumerable<SaleEntity>> GetQueryEnumerableAsync(Expression<Func<Sale, bool>> filter)
    {
        return await _dbContext.Sale
            .Where(filter)
            .Select(c => new SaleEntity
            {
                SaleID = c.SaleID,
                TotalPaid = c.TotalPaid,
                Order = new OrderEntity
                {
                    OrderID = c.Order.OrderID,
                    TotalAmount = c.Order.TotalAmount,
                    CustomerName = c.Order.CustomerName
                },
                Payments = c.Payments.Select(p => new SalePaymentEntity
                {
                    SalePaymentID = p.SalePaymentID,
                    Amount = p.Amount,
                    PaymentMethodID = p.PaymentMethodID,
                    PaymentStatus = new Domain.Entities.BaseEntities.PaymentStatusEntity
                    {
                        Description = p.PaymentStatus.Description
                    },
                }).ToList(),
                CreatedAt = c.CreatedAt,
                CreatedBy = c.CreatedBy
            })
            .ToListAsync();
    }
}
