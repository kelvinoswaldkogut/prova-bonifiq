using ProvaPub.Strategies.Factory.Interfaces;
using ProvaPub.Strategies.Interfaces;

namespace ProvaPub.Strategies.Factory
{
    public class PaymentFactory : IPaymentFactory
    {
        private readonly Dictionary<string, IPayment> _patternDiv;
        public PaymentFactory()
        {
            _patternDiv = new Dictionary<string, IPayment>(StringComparer.OrdinalIgnoreCase)// verificar se é isso mesmo. Stackover.
           {
               {"Pix", new PixPaymentType() },
               {"Credito", new CreditPaymentType() },
               {"Debito", new DebitPaymentType() },
               {"Voucher", new VoucherPaymentType() }
           };
        }
        public IPayment GetPaymentStrategy(string paymentType)
        {
            if (_patternDiv.TryGetValue(paymentType, out var payment))
            {
                return payment;
            }
            else
            {
                throw new InvalidOperationException($"Tipo de pagamento {paymentType}, não existe");
            }
        }
    }

}