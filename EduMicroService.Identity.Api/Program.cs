using Azure.Core;
using EduMicroService.Identity.Api;
using EduMicroService.Identity.Api.Features.Roles;
using EduMicroService.Identity.Api.Features.Users;
using EduMicroService.Identity.Api.Identity;
using EduMicroService.Identity.Api.Models;
using EduMicroService.Identity.Api.Options;
using EduMicroService.Identity.Api.Repositories;
using EduMicroService.Shared.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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



    builder.Services.AddOpenApi();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddCommonServiceExt(typeof(IdentityAssembly));
    builder.Services.AddVersioningExt();

    builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));
    builder.Services.Configure<List<ClientTokenOption>>(builder.Configuration.GetSection("Clients"));


    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));




    builder.Services.AddIdentity<AppUser, AppRole>(options =>
    {
        options.Password.RequiredLength = 4;
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.User.RequireUniqueEmail = true;
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    })
        .AddErrorDescriber<TurkishIdentityErrorDescriber>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();






    //builder.Services.AddAuthorization();
    //builder.Services.AddAuthentication();

    //Todo: buraya ileride bir policy eklenebilir
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    });





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

    app.EnsureSeedData();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.UseSwagger();
        app.UseSwaggerUI();
    }





    app.AddAuthsEndpointExt(app.AddVersionSetExt());
    app.AddUsersEndpointExt(app.AddVersionSetExt()).RequireAuthorization("Admin");
    app.AddRolesEndpointExt(app.AddVersionSetExt());


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
