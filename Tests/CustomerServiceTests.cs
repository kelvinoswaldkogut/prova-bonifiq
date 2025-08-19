using ProvaPub.Models;
using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using ProvaPub.Persistence.Interfaces;
using ProvaPub.Services;
using Bogus;
namespace ProvaPub.Tests
{
    public class CustomerServiceTests
    {


        private readonly Mock<IReader> _readerMock;
        private readonly CustomerService _service;



        public CustomerServiceTests()
        {
            _readerMock = new Mock<IReader>();
            _service = new CustomerService(_readerMock.Object);
        }

        [Fact]
        public async Task Should_Throw_When_CustomerId_IsInvalid()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _service.CanPurchase(0, 50));
        }

        [Fact]
        public async Task Should_Throw_When_PurchaseValue_IsInvalid()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _service.CanPurchase(1, 150));
        }



        [Fact]
        public async Task Should_Return_False_if_AlreadyPurchased_ThisMonth()
        {
            _readerMock.Setup(r => r.FindCustomerById(1))
                       .ReturnsAsync(new Customer { Id = 1, Name = "Kelvin" });

            _readerMock.Setup(r => r.GetOrdersInthisMonthByCustomersIdandDate(1, It.IsAny<DateTime>()))
                       .ReturnsAsync(1);

            var result = await _service.CanPurchase(1, 50);

            Assert.False(result);
        }

        [Fact]
        public async Task Should_ReturnFalse_When_FirstPurchase_And_ValueGreaterThan100()
        {
            _readerMock.Setup(r => r.FindCustomerById(1))
                       .ReturnsAsync(new Customer { Id = 1, Name = "Kelvin" });

            _readerMock.Setup(r => r.GetOrdersInthisMonthByCustomersIdandDate(1, It.IsAny<DateTime>()))
                       .ReturnsAsync(0);

            _readerMock.Setup(r => r.VerifyFirstBought(1))
                       .ReturnsAsync(false);

            var result = await _service.CanPurchase(1, 150);

            Assert.False(result);
        }


        [Fact]
        public async Task Should_ReturnTrue_When_AllRulesPass()
        {
            _readerMock.Setup(r => r.FindCustomerById(1))
                       .ReturnsAsync(new Customer { Id = 1 });

            _readerMock.Setup(r => r.GetOrdersInthisMonthByCustomersIdandDate(1, It.IsAny<DateTime>()))
                       .ReturnsAsync(0);

            _readerMock.Setup(r => r.VerifyFirstBought(1))
                       .ReturnsAsync(true);


            var result = await _service.CanPurchase(1, 50);

            Assert.True(result);
        }

        //tearia para testar as datas 

        [Theory]
        [InlineData(7, DayOfWeek.Monday, false)]
        [InlineData(8, DayOfWeek.Monday, true)]
        [InlineData(12, DayOfWeek.Monday, true)]
        [InlineData(19, DayOfWeek.Monday, false)]
        [InlineData(12, DayOfWeek.Saturday, false)]
        [InlineData(12, DayOfWeek.Sunday, false)]
        public void VerifyOutHour_ShouldReturnExpectedResult(int hour, DayOfWeek dayOfWeek, bool expected)
        {
            var testDate = new DateTime(2025, 8, 19, hour, 0, 0);
            testDate = testDate.AddDays((int)dayOfWeek - (int)testDate.DayOfWeek);

            var result = VerifyOutHour(testDate);

            Assert.Equal(expected, result);
        }


        public bool VerifyOutHour(DateTime now)
        {
            if (now.Hour < 8 || now.Hour > 18 || now.DayOfWeek == DayOfWeek.Saturday || now.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }
            return true;
        }

    }
}



