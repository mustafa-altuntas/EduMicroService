using EduMicroService.Payment.Api;
using EduMicroService.Shared.Extensions;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

 
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Payment API",
        Version = "v1",
        Description = "Edu Micro Service platformu için API",
        Contact = new OpenApiContact
        {
            Name = "Destek Ekibi",
            Email = "destek@edumicroservice.com"
        }
    });

    //// XML yorumlarýný dahil etmek için
    //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //c.IncludeXmlComments(xmlPath);
});

builder.Services.AddCommonServiceExt(typeof(PaymentAssembly));
builder.Services.AddVersioningExt();





var app = builder.Build();

 
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather API v1");
        c.RoutePrefix = "swagger"; // swagger UI'a eriþim yolu
    });
}

//app.AddFileEndpoints(app.AddVersionSetExt());



app.Run();

 