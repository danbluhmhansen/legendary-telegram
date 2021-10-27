namespace BlazorApp1.Server;

using Microsoft.AspNetCore.OData.Routing.Template;
using Microsoft.AspNetCore.OData.Routing;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

public class EntitySetWithKeySegment : ODataSegmentTemplate
{
	public override IEnumerable<string> GetTemplates(ODataRouteOptions options)
	{
		yield return "/{key}";
		// yield return "({key})"; enable it if you want to support key in parenthesis
	}

	public override bool TryTranslate(ODataTemplateTranslateContext context)
	{
		if (!context.RouteValues.TryGetValue("key", out _))
		{
			return false;
		}

		ODataPathSegment? previous = context.Segments.LastOrDefault();
		if (previous == null)
		{
			return false;
		}

		if (previous is not EntitySetSegment entitySet)
		{
			return false;
		}

		IEdmEntityType entityType = entitySet.EntitySet.EntityType();

		// Since we don't use the key values, it's ok to make an empty key value dictionary.
		IDictionary<string, object> keysValues = new Dictionary<string, object>();
		KeySegment keySegment = new KeySegment(keysValues, entityType, entitySet.EntitySet);

		context.Segments.Add(keySegment);
		return true;
	}
}
