using EduMicroService.Catalog.Api;
using EduMicroService.Catalog.Api.Features.Categories;
using EduMicroService.Catalog.Api.Options;
using EduMicroService.Catalog.Api.Repositories;
using EduMicroService.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptionsExt();
builder.Services.AddDatabaseServiceExt();
builder.Services.AddCommonServiceExt(typeof(CatalogAssembly));




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.AddCategoryEndpointExt();



app.Run();


