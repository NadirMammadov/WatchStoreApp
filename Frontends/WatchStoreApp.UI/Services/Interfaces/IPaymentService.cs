using WatchStoreApp.UI.Models.Payment;

namespace WatchStoreApp.UI.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput);
    }
}
