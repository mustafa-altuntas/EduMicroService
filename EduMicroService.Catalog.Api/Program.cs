using EduMicroService.Catalog.Api;
using EduMicroService.Catalog.Api.Features.Categories;
using EduMicroService.Catalog.Api.Features.Courses;
using EduMicroService.Catalog.Api.Options;
using EduMicroService.Catalog.Api.Repositories;
using EduMicroService.Shared.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Globalization;
using System.Text;



Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
    .CreateBootstrapLogger();

Log.Information("Starting up");


try
{

    var builder = WebApplication.CreateBuilder(args);


    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", formatProvider: CultureInfo.InvariantCulture)
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));



    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddOptionsExt();
    builder.Services.AddDatabaseServiceExt();
    builder.Services.AddCommonServiceExt(typeof(CatalogAssembly));
    builder.Services.AddVersioningExt();





    builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));
    //builder.Services.Configure<List<ClientTokenOption>>(builder.Configuration.GetSection("Clients"));



    builder.Services.AddAuthorization();
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
    {
        var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>();
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions!.SecurityKey));

        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            ValidIssuer = tokenOptions.Issuer,
            ValidAudiences = tokenOptions.Audiences,
            IssuerSigningKey = securityKey,

            ValidateIssuerSigningKey = true,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });



    var app = builder.Build();

    Task task = app.AddSeedDataExt()
        .ContinueWith(task =>
        {
            // Task continuation // await ile beklemek yerine task bitince aþaðýdaki kodu çalýþtýr diyoruz bu sayede bu method 5 dk dahi sürse uygula bloklanmýyor AddSeedDataExt() methodu arka planda çalýþýyor
            Console.WriteLine(task.IsFaulted ? task.Exception?.Message : "Seed data has been saved successfully");
        });




    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }


    app.AddCategoryEndpointExt(app.AddVersionSetExt()).RequireAuthorization();
    app.AddCourseEndpointExt(app.AddVersionSetExt()).RequireAuthorization();




    app.Run();
}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}
