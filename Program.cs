using Microsoft.EntityFrameworkCore;
using ProvaPub.Extensions;
using ProvaPub.Persistence.ChangesNSaves;
using ProvaPub.Persistence.Interfaces;
using ProvaPub.Repository;
using ProvaPub.Services;
using ProvaPub.Services.Interfaces;
using ProvaPub.Strategies;
using ProvaPub.Strategies.Contexto;
using ProvaPub.Strategies.Factory;
using ProvaPub.Strategies.Factory.Interfaces;
using ProvaPub.Strategies.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<RandomService>();

builder.Services.AddTransient<IPayment, PixPaymentType>();
builder.Services.AddTransient<IPayment, DebitPaymentType>();
builder.Services.AddTransient<IPayment, CreditPaymentType>();
builder.Services.AddTransient<IPayment, VoucherPaymentType>();

builder.Services.AddTransient<IPaymentContext, PaymentContext>();
builder.Services.AddTransient<IPaymentFactory, PaymentFactory>();

builder.Services.AddTransient<IWriter, Writer>();
builder.Services.AddTransient<IReader, Reader>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IRandomService, RandomService>();
builder.Services.AddTransient<IPaymentService, PaymentServices>();
builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddDbContext<TestDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("ctx")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
