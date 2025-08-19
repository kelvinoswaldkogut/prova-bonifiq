using Bogus;
using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Persistence.Interfaces;
using ProvaPub.Repository;
using ProvaPub.Services.Interfaces;
using System.Formats.Asn1;

namespace ProvaPub.Services
{
    public class CustomerService : ICustomerService
    {

        private readonly IReader _reader;
        public CustomerService(IReader reader)
        {
            _reader = reader;
        }

        public async Task<PagLists<Customer>> ListCustomers(int page)
        {
            var customerResponse = new PagLists<Customer>();
            var customersList = await _reader.GetListCustomersAsync(page);

            
            if (customersList != null)
            {
                var pagedCustomers = customersList
                .Skip((page - 1) * Rules.PageSize)
                .Take(Rules.PageSize)
                .ToList();
                customerResponse = new PagLists<Customer>()
                {
                    HasNext = customersList.Count > page * Rules.PageSize,
                    TotalCount = customersList.Count,
                    Itens = customersList
                };  
               

            }
            return customerResponse;

        }

        public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
        {
            if (customerId <= 0) throw new ArgumentOutOfRangeException(nameof(customerId));

            if (purchaseValue <= 0) throw new ArgumentOutOfRangeException(nameof(purchaseValue));

            var customer = await GetCustomer(customerId);
            if (customer == null) throw new InvalidOperationException($"Customer Id {customerId} does not exists");

            //Business Rule: A customer can purchase only a single time per month
            var ordersInThisMonth = await GetOrdersInThisMonth(customerId);
            if (!ordersInThisMonth)
                return false;

            //Business Rule: A customer that never bought before can make a first purchase of maximum 100,00
            var haveBoughtBefore = await VerifyCustomerBought(customerId, purchaseValue);
             if(!haveBoughtBefore)return false;

            //Business Rule: A customer can purchases only during business hours and working days
            var outOfHours = VerifyOutHour();
            if (!outOfHours)
                 return false;


            return true;
        }
        public async Task<Customer> GetCustomer(int customerId)
        {
            var customer = await _reader.FindCustomerById(customerId);
            return customer;
        }

        public bool VerifyOutHour()
        {
            var now = DateTime.UtcNow;
            if (now.Hour < 8 || now.Hour > 18 || now.DayOfWeek == DayOfWeek.Saturday || now.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> VerifyCustomerBought(int customersId, decimal purchaseValue)
        {
            var haveBoughtBefore = await _reader.VerifyFirstBought(customersId);
            if (!haveBoughtBefore && purchaseValue > 100)
            {
                return false;
            }
            else
            {
                return true;
            }
                
        }
        public async Task<bool> GetOrdersInThisMonth(int customersId)
        {
            var baseDate = DateTime.UtcNow.AddMonths(-1);
            var ordersInThisMonth = await _reader.GetOrdersInthisMonthByCustomersIdandDate(customersId, baseDate);

            if (ordersInThisMonth > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
                
        }
        public static class Rules
        {
            public static int PageSize = 10;
        }


    }
}
