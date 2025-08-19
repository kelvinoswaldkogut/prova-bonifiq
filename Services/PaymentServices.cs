using ProvaPub.Persistence.Interfaces;
using ProvaPub.Services.Interfaces;
using ProvaPub.Strategies.Contexto;

namespace ProvaPub.Services
{
    public class PaymentServices : IPaymentService
    {
        private readonly IWriter _writer;
        private readonly IPaymentContext _context;
        public PaymentServices(IWriter writer, IPaymentContext context)
        {
            _context = context;
            _writer = writer;
        }
        public async Task<string> PaymentProcess(decimal value, string paymentMethod, int CustomerId)
        {
            var resp = await _context.PaymentApply(paymentMethod, value);

            return resp;
        }

    }
}
