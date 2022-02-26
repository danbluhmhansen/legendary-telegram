using Audit.Core;
using Audit.EntityFramework;
using Audit.EntityFramework.ConfigurationApi;
using Audit.EntityFramework.Providers;

using AutoMapper;

using BlazorApp1.OData.Model;
using BlazorApp1.Server;
using BlazorApp1.Server.Data;
using BlazorApp1.Server.Entities;

using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Batch;
using Microsoft.EntityFrameworkCore;

using OpenIddict.Server;
using OpenIddict.Server.AspNetCore;
using OpenIddict.Validation;

using Quartz;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(
    (IServiceProvider serviceProvider, DbContextOptionsBuilder options) => options
        .UseNpgsql(connectionString)
        .AddInterceptors(serviceProvider.GetRequiredService<CustomAuditSaveChangesInterceptor>())
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

// TODO: Implement.
// Audit.EntityFramework.Configuration.Setup()
//     .ForContext((IContextSettingsConfigurator<ApplicationDbContext> _) => {})
//     .UseOptOut()
//     .Ignore<AuditLog>()
//     .Ignore<OpenIddictEntityFrameworkCoreAuthorization>()
//     .Ignore<OpenIddictEntityFrameworkCoreScope>()
//     .Ignore<OpenIddictEntityFrameworkCoreToken>();

builder.Services.AddScoped<IAuditScopeFactory>(_ => new AuditScopeFactory());
builder.Services.AddScoped<AuditDataProvider>((IServiceProvider serviceProvider) => new EntityFrameworkDataProvider(
    (IEntityFrameworkProviderConfigurator config) => config
        .UseDbContext((AuditEventEntityFramework _) => serviceProvider.GetRequiredService<ApplicationDbContext>())
        .AuditTypeMapper((Type _) => typeof(AuditLog))
        .AuditEntityAction((AuditEvent auditEvent, EventEntry entry, AuditLog entity) =>
        {
            entity.AuditType = entry.EntityType.FullName;
            entity.AuditDate = auditEvent.StartDate;
            entity.EntityKey = Audit.Core.Configuration.JsonAdapter.Serialize(entry.PrimaryKey);
            entity.EntityData = entry.ToJson();

            // FIXME: HttpContext is null here.
            IHttpContextAccessor httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            if (httpContextAccessor.HttpContext is not null)
            {
                UserManager<ApplicationUser> userManager = serviceProvider
                    .GetRequiredService<UserManager<ApplicationUser>>();
                entity.AuditUserId = userManager.GetUserId(httpContextAccessor.HttpContext.User);
            }
        })
        .IgnoreMatchedProperties()));
builder.Services.AddScoped<IAuditDbContext>((IServiceProvider serviceProvider) =>
{
    DefaultAuditContext context = new(serviceProvider.GetRequiredService<ApplicationDbContext>());
    context.AuditScopeFactory = serviceProvider.GetRequiredService<IAuditScopeFactory>();
    context.AuditDataProvider = serviceProvider.GetRequiredService<AuditDataProvider>();
    return context;
});
builder.Services.AddScoped((IServiceProvider serviceProvider) =>
{
    DbContextHelper helper = new();
    helper.SetConfig(serviceProvider.GetRequiredService<IAuditDbContext>());
    return helper;
});
builder.Services.AddScoped<CustomAuditSaveChangesInterceptor>();

builder.Services.Configure<IdentityOptions>(builder.Configuration.GetSection(nameof(IdentityOptions)));
builder.Services.Configure<ODataOptions>(builder.Configuration.GetSection(nameof(ODataOptions)));
builder.Services.Configure<OpenIddictServerOptions>(builder.Configuration.GetSection(nameof(OpenIddictServerOptions)));
builder.Services.Configure<OpenIddictServerAspNetCoreOptions>(
    builder.Configuration.GetSection(nameof(OpenIddictServerAspNetCoreOptions)));
builder.Services.Configure<OpenIddictValidationOptions>(
    builder.Configuration.GetSection(nameof(OpenIddictValidationOptions)));

WebApplication app = builder.Build();

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
