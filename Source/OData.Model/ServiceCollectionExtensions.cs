namespace BlazorApp1.OData.Model;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddODataModel(this IServiceCollection services) =>
		services.AddSingleton<IODataModelProvider, ODataModelProvider>();
}
