using ProvaPub.Strategies.Interfaces;

namespace ProvaPub.Strategies
{
    public class PixPaymentType : IPayment
    {
        public async Task<string> InsertPayment(decimal value)
        {
            await Task.Delay(80);
            return $"Pagamento realizado com pix valor: {value}";
        }
    }
}
