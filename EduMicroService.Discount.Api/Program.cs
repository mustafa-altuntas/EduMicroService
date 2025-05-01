using EduMicroService.Discount.Api;
using EduMicroService.Discount.Api.Features.Discounts;
using EduMicroService.Discount.Api.Options;
using EduMicroService.Discount.Api.Repositories;
using EduMicroService.Shared.Extensions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommonServiceExt(typeof(DiscountAssembly));
builder.Services.AddVersioningExt();

builder.Services.AddOptionsExt();
builder.Services.AddDatabaseServiceExt();











var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.AddDiscountEndpointExt(app.AddVersionSetExt());



app.Run();

 