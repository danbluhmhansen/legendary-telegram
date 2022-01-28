namespace BlazorApp1.OData.Model;

using Microsoft.OData.Edm;

public interface IODataModelProvider
{
	IEdmModel GetEdmModel(string apiVersion);
}
