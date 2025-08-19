using Microsoft.EntityFrameworkCore;
using Moq;
using ProvaPub.Models;
using ProvaPub.Persistence.ChangesNSaves;
using ProvaPub.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class ReaderTests
{

    private Mock<DbSet<Product>> _productsMock;
    private Mock<DbSet<Customer>> _customersMock;
    private Mock<DbSet<Order>> _ordersMock;
    private Mock<TestDbContext> _contextMock;
    private Reader _reader;

    public ReaderTests()
    {

        _productsMock = new Mock<DbSet<Product>>();
        _customersMock = new Mock<DbSet<Customer>>();
        _ordersMock = new Mock<DbSet<Order>>();

        _contextMock = new Mock<TestDbContext>();

        _contextMock.Setup(c => c.Products).Returns(_productsMock.Object);
        _contextMock.Setup(c => c.Customers).Returns(_customersMock.Object);
        _contextMock.Setup(c => c.Orders).Returns(_ordersMock.Object);

        _reader = new Reader(_contextMock.Object);
    }

    [Fact]
    public async Task FindCustomerById_ReturnsCustomer_WhenExists()
    {
        var customer = new Customer { Id = 1, Name = "Maria" };
        _contextMock.Setup(c => c.Customers.FindAsync(1))
            .ReturnsAsync(customer);

        var result = await _reader.FindCustomerById(1);

        Assert.Equal("Maria", result.Name);
    }
    [Fact]
    public async Task GetOrdersInthisMonthByCustomersIdandDate_ReturnsCount()
    {
        var baseDate = new DateTime(2025, 8, 1);

        var data = new List<Order>
    {
        new Order { CustomerId = 1, OrderDate = new DateTime(2025, 8, 10) },
        new Order { CustomerId = 1, OrderDate = new DateTime(2025, 7, 10) }
    }.AsQueryable();

        _ordersMock.As<IQueryable<Order>>().Setup(m => m.Provider).Returns(data.Provider);
        _ordersMock.As<IQueryable<Order>>().Setup(m => m.Expression).Returns(data.Expression);
        _ordersMock.As<IQueryable<Order>>().Setup(m => m.ElementType).Returns(data.ElementType);
        _ordersMock.As<IQueryable<Order>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var result = await _reader.GetOrdersInthisMonthByCustomersIdandDate(1, baseDate);

        Assert.Equal(1, result);
    }
    [Fact]
    public async Task VerifyFirstBought_ReturnsTrue_WhenCustomerHasOrders()
    {
        var data = new List<Order> { new Order { CustomerId = 1, OrderDate = DateTime.Now } }.AsQueryable();
        _ordersMock.As<IQueryable<Order>>().Setup(m => m.Provider).Returns(data.Provider);
        _ordersMock.As<IQueryable<Order>>().Setup(m => m.Expression).Returns(data.Expression);
        _ordersMock.As<IQueryable<Order>>().Setup(m => m.ElementType).Returns(data.ElementType);
        _ordersMock.As<IQueryable<Order>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var result = await _reader.VerifyFirstBought(1);

        Assert.True(result);
    }

}
