using ProvaPub.Strategies.Factory.Interfaces;
using ProvaPub.Strategies.Interfaces;

namespace ProvaPub.Strategies.Contexto
{
    public class PaymentContext : IPaymentContext
    {
         private readonly IDictionary<string, IPayment> _payments;
         private readonly IPaymentFactory _paymentFactory;
         public PaymentContext(IEnumerable<IPayment> payments, IPaymentFactory paymentFac)
        {
            if (payments == null || !payments.Any())
                throw new ArgumentException("Nenhm tipo de pagamento encontrado");
            
            _payments = payments.ToDictionary(p => p.GetType().Name.Replace("Payment", "").ToUpper(), p => p);
            _paymentFactory = paymentFac;
        }
        public async Task<string> PaymentApply(string paymentType, decimal amount)
        {
           var payment = _paymentFactory.GetPaymentStrategy(paymentType);
           var result = await payment.InsertPayment(amount);

            return result.ToString();
        }
    }
}
