namespace BlazorApp1.Server;

using Microsoft.AspNetCore.OData.Formatter.Serialization;

public class CustomODataSerializerProvider : ODataSerializerProvider
{
	public CustomODataSerializerProvider(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}
}
