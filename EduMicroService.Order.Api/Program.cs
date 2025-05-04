using EduMicroService.Order.Api.Endpoints.Orders;
using EduMicroService.Order.Application;
using EduMicroService.Order.Application.Contracts.Repositories;
using EduMicroService.Order.Application.Contracts.UnitOfWork;
using EduMicroService.Order.Persistence;
using EduMicroService.Order.Persistence.Repositories;
using EduMicroService.Order.Persistence.UnitOfWork;
using EduMicroService.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCommonServiceExt(typeof(OrderApplicationAssembly));
builder.Services.AddVersioningExt();









var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}


app.AddOrderEndpointExt(app.AddVersionSetExt());


app.Run();

