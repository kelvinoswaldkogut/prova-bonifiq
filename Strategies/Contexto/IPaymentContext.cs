namespace ProvaPub.Strategies.Contexto
{
    public interface IPaymentContext
    {
        Task<string> PaymentApply(string paymentType, decimal amount);
    }
}
