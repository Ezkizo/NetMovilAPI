namespace NetMovilAPI.Application.Presenters.ViewModels
{
    public class SaleViewModel
    {
        public int SaleID { get; set; }
        public decimal TotalPaid { get; set; }
        public int OrderID { get; set; }
        public decimal TotalAmount { get; set; }
        public string? CustomerName { get; set; }
        public List<SalePaymentViewModel> Payments { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
    }

    public class SalePaymentViewModel
    {
        public int SalePaymentID { get; set; }
        public decimal Amount { get; set; }
        public int PaymentMethodID { get; set; }
        public string PaymentStatus { get; set; }
    }

}
