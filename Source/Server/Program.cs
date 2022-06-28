using LegendaryTelegram.Server.Data;
using LegendaryTelegram.Server.Interfaces;
using LegendaryTelegram.Server.Models;
using LegendaryTelegram.Server.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using OpenIddict.Server;
using OpenIddict.Server.AspNetCore;
using OpenIddict.Validation;

using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddLogging();

builder.Services.AddDbContext<ApplicationDbContext>(options => options
    .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseOpenIddict());

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddOpenIddict()
    .AddCore(options => options.UseEntityFrameworkCore().UseDbContext<ApplicationDbContext>())
    .AddServer(options =>
    {
        options.UseAspNetCore();

        if (builder.Environment.IsDevelopment())
            options.AddDevelopmentEncryptionCertificate().AddDevelopmentSigningCertificate();
    })
    .AddValidation(options =>
    {
        options.UseLocalServer();
        options.UseAspNetCore();
    });

builder.Services.AddControllersWithViews();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    OpenApiSecurityScheme securityScheme = new()
    {
        Type = SecuritySchemeType.OpenIdConnect,
        OpenIdConnectUrl = new Uri("/.well-known/openid-configuration", UriKind.Relative),
    };
    options.AddSecurityDefinition("openid", securityScheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        [securityScheme] = Array.Empty<string>(),
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>(true, "openid");
});

builder.Services.AddSingleton<IODataModelProvider, ODataModelProvider>();

if (builder.Environment.IsDevelopment())
    builder.Services.AddHostedService<SeedWorker>();

builder.Services.Configure<IdentityOptions>(builder.Configuration.GetSection(nameof(IdentityOptions)));
builder.Services.Configure<OpenIddictServerOptions>(builder.Configuration.GetSection(nameof(OpenIddictServerOptions)));
builder.Services.Configure<OpenIddictServerAspNetCoreOptions>(
    builder.Configuration.GetSection(nameof(OpenIddictServerAspNetCoreOptions)));
builder.Services.Configure<OpenIddictValidationOptions>(
    builder.Configuration.GetSection(nameof(OpenIddictValidationOptions)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days.
    // You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.OAuthClientId("sample-client");
        options.OAuthClientSecret("3e52ce50-5ac2-4885-94d8-2c8a6c1c902a");
        options.OAuthUsePkce();
    });
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseStatusCodePagesWithReExecute("/error");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapDefaultControllerRoute();

app.Run();
