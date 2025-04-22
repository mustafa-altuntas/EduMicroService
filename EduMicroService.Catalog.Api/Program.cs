using EduMicroService.Catalog.Api;
using EduMicroService.Catalog.Api.Features.Categories;
using EduMicroService.Catalog.Api.Features.Courses;
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

Task task = app.AddSeedDataExt()
    .ContinueWith(task =>
    {
        // Task continuation // await ile beklemek yerine task bitince a�a��daki kodu �al��t�r diyoruz bu sayede bu method 5 dk dahi s�rse uygula bloklanm�yor AddSeedDataExt() methodu arka planda �al���yor
        Console.WriteLine(task.IsFaulted ? task.Exception?.Message : "Seed data has been saved successfully");
    });




if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.AddCategoryEndpointExt();
app.AddCourseEndpointExt();



app.Run();


