using NetMovilAPI.Application.Presenters.ViewModels;
using NetMovilAPI.Domain.Entities.Sale;
using NetMovilAPI.Domain.Interfaces;

namespace NetMovilAPI.Application.Presenters;
public class SalePresenter : IPresenter<SaleEntity, SaleViewModel>
{
    public SaleViewModel Present(SaleEntity data)
    {
        if (data == null)
            return null!;

        return new SaleViewModel
        {
            SaleID = data.SaleID,
            TotalPaid = data.TotalPaid,
            OrderID = data.Order?.OrderID ?? 0,
            TotalAmount = data.Order?.TotalAmount ?? 0,
            CustomerName = data.Order?.CustomerName,
            Payments = data.Payments?.Select(p => new SalePaymentViewModel
            {
                SalePaymentID = p.SalePaymentID,
                Amount = p.Amount,
                PaymentMethodID = p.PaymentMethodID,
                PaymentMehod = p.PaymentMehod,
                PaymentStatusID = p.PaymentStatusID,
                PaymentStatus = p.PaymentStatus
            }).ToList() ?? [],
            CreatedAt = data.CreatedAt,
            CreatedBy = data.CreatedBy,
            UpdatedAt = data.UpdatedAt,
            UpdatedBy = data.CreatedBy
        };
    }

    public IEnumerable<SaleViewModel> Present(IEnumerable<SaleEntity> data) => [.. data.Select(Present)];
    
}
