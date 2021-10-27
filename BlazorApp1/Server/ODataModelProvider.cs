namespace BlazorApp1.Server;

using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

public class ODataModelProvider
{
	private readonly IDictionary<string, IEdmModel> cached = new Dictionary<string, IEdmModel>();

	public IEdmModel GetEdmModel(string apiVersion)
	{
		if (this.cached.TryGetValue(apiVersion, out IEdmModel? model) && model is not null)
			return model;

		model = BuildEdmModel(apiVersion);
		this.cached[apiVersion] = model;
		return model;
	}

	private static IEdmModel BuildEdmModel(string version) =>
		version switch
		{
			"1.0" => BuildV1Model(),
			"2.0" => BuildV2Model(),
			_ => throw new NotSupportedException($"The input version '{version}' is not supported!"),
		};

	private static IEdmModel BuildV1Model()
	{
		ODataConventionModelBuilder builder = new();
		builder.EntitySet<Models.v1.Customer>("Customers");

		return builder.GetEdmModel();
	}

	private static IEdmModel BuildV2Model()
	{
		ODataConventionModelBuilder builder = new();
		builder.EntitySet<Models.v2.Customer>("Customers");

		return builder.GetEdmModel();
	}
}
