using ProvaPub.Strategies.Interfaces;

namespace ProvaPub.Strategies
{
    public class DebitPaymentType : IPayment
    {
        public async Task<string> InsertPayment(decimal value)
        {
            await Task.Delay(80);
            return $"Pagamento realizado com cartão de de debito valor: {value}";
        }
    }
}
