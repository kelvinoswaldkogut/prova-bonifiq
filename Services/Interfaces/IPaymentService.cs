namespace ProvaPub.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<string> PaymentProcess(decimal value, string paymentMethod, int CustomerId);
    }
}
