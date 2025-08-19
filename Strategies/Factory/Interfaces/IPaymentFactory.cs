using ProvaPub.Strategies.Interfaces;

namespace ProvaPub.Strategies.Factory.Interfaces
{
    public interface IPaymentFactory
    {
        IPayment GetPaymentStrategy(string paymentType);
    }
}
