using AutoMapper;

using BlazorApp1.Server;
using BlazorApp1.Server.Data;
using BlazorApp1.Server.Entities;

using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Batch;
using Microsoft.AspNetCore.OData.Formatter.Serialization;
using Microsoft.AspNetCore.OData.Routing.Conventions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

using OpenIddict.Server;
using OpenIddict.Server.AspNetCore;
using OpenIddict.Validation;

using Quartz;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(
	(DbContextOptionsBuilder options) => options.UseNpgsql(connectionString));
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

ODataConventionModelBuilder oDataBuilder = new();
oDataBuilder.EntitySet<BlazorApp1.Shared.Models.v1.Character>("Characters");
oDataBuilder.EntitySet<BlazorApp1.Shared.Models.v1.Feature>("Features");
oDataBuilder.EntitySet<BlazorApp1.Shared.Models.v1.CoreEffect>("CoreEffects");
oDataBuilder.EntitySet<BlazorApp1.Shared.Models.v1.Effect>("Effects");
EntitySetConfiguration<BlazorApp1.Shared.Models.v1.CharacterFeature> characterFeaturesConfiguration = oDataBuilder
	.EntitySet<BlazorApp1.Shared.Models.v1.CharacterFeature>("CharacterFeatures");
characterFeaturesConfiguration.EntityType
	.HasKey((BlazorApp1.Shared.Models.v1.CharacterFeature characterFeature) =>
		new { characterFeature.CharacterId, characterFeature.FeatureId });
IEdmModel edmModel = oDataBuilder.GetEdmModel();

builder.Services
	.AddControllersWithViews()
	.AddOData((ODataOptions options) =>
	{
		options.AddRouteComponents("v1", edmModel, (IServiceCollection services) =>
		{
			services.AddSingleton(new DefaultODataBatchHandler());
			services.AddSingleton<ODataResourceSerializer, CustomODataResourceSerializer>();
		});

		IODataControllerActionConvention? oDataConvention = options.Conventions
			.FirstOrDefault((IODataControllerActionConvention convention) =>
				convention.GetType() == typeof(EntitySetRoutingConvention));
		if (oDataConvention is not null)
			options.Conventions.Remove(oDataConvention);

		options.Conventions.Add(new CustomEntitySetRoutingConvention());
	});
builder.Services.AddRazorPages();

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
