using Audit.Core;
using Audit.EntityFramework;
using Audit.EntityFramework.ConfigurationApi;

using AutoMapper;

using BlazorApp1.OData.Model;
using BlazorApp1.Server;
using BlazorApp1.Server.Data;
using BlazorApp1.Server.Entities;

using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Batch;
using Microsoft.AspNetCore.OData.Formatter.Serialization;
using Microsoft.EntityFrameworkCore;

using OpenIddict.EntityFrameworkCore.Models;
using OpenIddict.Server;
using OpenIddict.Server.AspNetCore;
using OpenIddict.Validation;

using Quartz;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>((DbContextOptionsBuilder options) => options
    .UseNpgsql(connectionString)
    .AddInterceptors(new AuditSaveChangesInterceptor())
    );
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddQuartz((IServiceCollectionQuartzConfigurator options) =>
{
    options.UseMicrosoftDependencyInjectionJobFactory();
    options.UseSimpleTypeLoader();
    options.UseInMemoryStore();
});

builder.Services.AddOpenIddict((OpenIddictBuilder openIddictBuilder) =>
{
    openIddictBuilder.AddCore((OpenIddictCoreBuilder openIddictCoreBuilder) =>
    {
        openIddictCoreBuilder
            .UseEntityFrameworkCore()
            .UseDbContext<ApplicationDbContext>();

        openIddictCoreBuilder.UseQuartz();
    });

    openIddictBuilder.AddServer()
        .AddDevelopmentEncryptionCertificate()
        .AddDevelopmentSigningCertificate()
        .UseAspNetCore();

    openIddictBuilder.AddValidation((OpenIddictValidationBuilder options) =>
    {
        options.UseLocalServer();
        options.UseAspNetCore();
    });
});

builder.Services.AddAutoMapper((IMapperConfigurationExpression expression) =>
{
    expression.CreateMap<BlazorApp1.Server.Entities.Character, BlazorApp1.Shared.Models.v1.Character>().ReverseMap();
    expression.CreateMap<BlazorApp1.Server.Entities.Feature, BlazorApp1.Shared.Models.v1.Feature>().ReverseMap();
    expression.CreateMap<BlazorApp1.Server.Entities.CoreEffect, BlazorApp1.Shared.Models.v1.CoreEffect>().ReverseMap();
    expression.CreateMap<BlazorApp1.Server.Entities.Effect, BlazorApp1.Shared.Models.v1.Effect>().ReverseMap();
    expression.CreateMap<BlazorApp1.Server.Entities.CharacterFeature, BlazorApp1.Shared.Models.v1.CharacterFeature>()
        .ReverseMap();
});

builder.Services
    .AddControllersWithViews()
    .AddOData((ODataOptions options, IServiceProvider serviceProvider) =>
    {
        IODataModelProvider odataModelProvider = serviceProvider.GetRequiredService<IODataModelProvider>();
        options.AddRouteComponents("v1", odataModelProvider.GetEdmModel("1"), (IServiceCollection services) =>
            services.AddSingleton<ODataBatchHandler>(new DefaultODataBatchHandler()));
    });
builder.Services.AddRazorPages();

builder.Services.AddHttpContextAccessor();

builder.Services.AddODataModel();

builder.Services.Configure<IdentityOptions>(builder.Configuration.GetSection(nameof(IdentityOptions)));
builder.Services.Configure<ODataOptions>(builder.Configuration.GetSection(nameof(ODataOptions)));
builder.Services.Configure<OpenIddictServerOptions>(builder.Configuration.GetSection(nameof(OpenIddictServerOptions)));
builder.Services.Configure<OpenIddictServerAspNetCoreOptions>(
    builder.Configuration.GetSection(nameof(OpenIddictServerAspNetCoreOptions)));
builder.Services.Configure<OpenIddictValidationOptions>(
    builder.Configuration.GetSection(nameof(OpenIddictValidationOptions)));

WebApplication app = builder.Build();

IServiceScope serviceScope = app.Services.CreateScope();

Audit.EntityFramework.Configuration.Setup()
    .ForContext((IContextSettingsConfigurator<ApplicationDbContext> _) => {})
    .UseOptOut()
    .Ignore<AuditLog>()
    .Ignore<OpenIddictEntityFrameworkCoreAuthorization>()
    .Ignore<OpenIddictEntityFrameworkCoreScope>()
    .Ignore<OpenIddictEntityFrameworkCoreToken>();

Audit.Core.Configuration.Setup()
    .UseEntityFramework((IEntityFrameworkProviderConfigurator config) => config
        .UseDbContext((AuditEventEntityFramework _) =>
            serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
        .AuditTypeMapper((Type _) => typeof(AuditLog))
        .AuditEntityAction((AuditEvent auditEvent, EventEntry entry, AuditLog entity) =>
        {
            entity.AuditType = entry.EntityType.FullName;
            entity.AuditDate = auditEvent.StartDate;
            entity.EntityKey = Audit.Core.Configuration.JsonAdapter.Serialize(entry.PrimaryKey);
            entity.EntityData = entry.ToJson();

            // FIXME: HttpContext is null here.
            IHttpContextAccessor httpContextAccessor = serviceScope.ServiceProvider
                .GetRequiredService<IHttpContextAccessor>();
            if (httpContextAccessor.HttpContext is not null)
            {
                UserManager<ApplicationUser> userManager = serviceScope.ServiceProvider
                    .GetRequiredService<UserManager<ApplicationUser>>();
                entity.AuditUserId = userManager.GetUserId(httpContextAccessor.HttpContext.User);
            }
        })
        .IgnoreMatchedProperties());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();

    app.UseCors((CorsPolicyBuilder corsPolicyBuilder) =>
        corsPolicyBuilder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());

    app.UseODataRouteDebug();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days.
    // You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseODataQueryRequest();
app.UseODataBatching();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.UseEndpoints((IEndpointRouteBuilder options) =>
    options.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}"));

app.Run();
