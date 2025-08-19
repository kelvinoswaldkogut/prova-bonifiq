using ProvaPub.Models;
using ProvaPub.Persistence.Interfaces;
using ProvaPub.Repository;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services
{
    public class OrderService : IOrderService
    {
        private readonly IPaymentService _paymentService;
        private readonly IWriter _writer;

        public OrderService(IPaymentService paymentService, IWriter writer)
        {
            _paymentService = paymentService;
            _writer = writer;
        }


        public async Task<string> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
        {
            var ret = await _paymentService.PaymentProcess(paymentValue, paymentMethod, customerId);
            if (ret != null)
            {
                var date = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                await _writer.InsertOrder(new Order()
                {
                    Value = paymentValue,
                    CustomerId = customerId,
                    OrderDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, date)
                });

                
            }
            return ret;
        }


    }
}
