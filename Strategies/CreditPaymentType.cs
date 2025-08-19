using ProvaPub.Strategies.Interfaces;

namespace ProvaPub.Strategies
{
    public class CreditPaymentType : IPayment
    {
        
       public async Task<string> InsertPayment(decimal value)
        {
            await Task.Delay(80);
            return $"Pagamento realizado com cartao de credito valor: {value}";
        }
    }
}
