namespace BlazorApp1.Server;

using Microsoft.AspNetCore.OData.Routing;
using Microsoft.AspNetCore.OData.Routing.Template;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

public class EntitySetCustomersSegment : ODataSegmentTemplate
{
	public override IEnumerable<string> GetTemplates(ODataRouteOptions options)
	{
		yield return "/Customers";
	}

	public override bool TryTranslate(ODataTemplateTranslateContext context)
	{
		// Support case-insenstivie
		IEdmEntitySet? edmEntitySet = context.Model.EntityContainer.EntitySets()
			.FirstOrDefault(e => string.Equals("Customers", e.Name, StringComparison.OrdinalIgnoreCase));

		if (edmEntitySet != null)
		{
			EntitySetSegment segment = new EntitySetSegment(edmEntitySet);
			context.Segments.Add(segment);
			return true;
		}

		return false;
	}
}
