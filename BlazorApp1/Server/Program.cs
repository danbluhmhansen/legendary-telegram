using BlazorApp1.Server;
using BlazorApp1.Server.Data;
using BlazorApp1.Server.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

using OpenIddict.Server;
using OpenIddict.Server.AspNetCore;
using OpenIddict.Validation;

using Quartz;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options
	.UseNpgsql(connectionString)
	.UseOpenIddict());
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();

builder.Services.AddQuartz(options =>
{
	options.UseMicrosoftDependencyInjectionJobFactory();
	options.UseSimpleTypeLoader();
	options.UseInMemoryStore();
});

builder.Services.AddOpenIddict(openIddictBuilder =>
{
	openIddictBuilder.AddCore(openIddictCoreBuilder =>
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

	openIddictBuilder.AddValidation(options =>
	{
		options.UseLocalServer();
		options.UseAspNetCore();
	});
});

builder.Services
	.AddControllersWithViews()
	.AddOData();
builder.Services.AddRazorPages();

builder.Services.TryAddSingleton<ODataModelProvider>();
builder.Services.TryAddEnumerable(
	ServiceDescriptor.Transient<IApplicationModelProvider, ODataRoutingApplicationModelProvider>());
builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<MatcherPolicy, ODataRoutingMatcherPolicy>());

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

	app.UseCors(corsPolicyBuilder => corsPolicyBuilder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());

	app.UseODataRouteDebug();
}
else
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.UseEndpoints(options => options.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}"));

app.Run();
