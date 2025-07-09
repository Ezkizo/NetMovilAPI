using NetMovilAPI.Application.DTOs.Requests;
using NetMovilAPI.Domain.Entities.BaseEntities;
using NetMovilAPI.Domain.Entities.Sale;
using NetMovilAPI.Domain.Interfaces;

namespace NetMovilAPI.Application.Mappers;
public class SaleMapper : IMapper<SaleRequestDTO, SaleEntity>
{
    public SaleEntity ToEntity(SaleRequestDTO sale)
    {
        return new SaleEntity
        {
            SaleID = sale.SaleID,
            TotalPaid = sale.TotalPaid,
            OrderID = sale.OrderID,
            Payments = sale.Payments?.Select(p => new SalePaymentEntity
            {
                SalePaymentID = p.SalePaymentID,
                Amount = p.Amount,
                Reference = p.Reference,
                SaleID = p.SaleID,
                PaymentMethodID = p.PaymentMethodID,
                PaymentStatusID = p.PaymentStatusID,
            }).ToList(),
            CreatedBy = sale.CreatedBy,
            CreatedAt = (DateTimeOffset)sale.CreatedAt!,
        };
    }

    public IEnumerable<SaleEntity> ToEntity(List<SaleRequestDTO> sales) => [..sales.Select(ToEntity)];
}
