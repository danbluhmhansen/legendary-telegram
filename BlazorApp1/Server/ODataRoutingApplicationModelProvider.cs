namespace BlazorApp1.Server;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Routing.Template;
using Microsoft.OData.Edm;

public class ODataRoutingApplicationModelProvider : IApplicationModelProvider
{
	private readonly ODataModelProvider modelProvider;

	public ODataRoutingApplicationModelProvider(ODataModelProvider modelProvider)
	{
		this.modelProvider = modelProvider;
	}

	public int Order => 90;

	public void OnProvidersExecuted(ApplicationModelProviderContext context)
	{
		var models = context.Result.Controllers
			.Where(controller => controller.HasAttribute<ApiVersionAttribute>())
			.SelectMany(controller => controller.GetAttribute<ApiVersionAttribute>().Versions)
			.Distinct()
			.Select(apiVersion => this.modelProvider.GetEdmModel(apiVersion.ToString()))
			.ToList();
		IEdmModel model = EdmCoreModel.Instance; // just for place holder
		string prefix = string.Empty;
		foreach (ControllerModel controllerModel in context.Result.Controllers)
		{
			// CustomersController
			if (controllerModel.ControllerName == "Customers")
			{
				ProcessCustomersController(prefix, model, controllerModel);
				continue;
			}

			// MetadataController
			if (controllerModel.ControllerName == "Metadata")
			{
				ProcessMetadata(prefix, model, controllerModel);
				continue;
			}
		}
	}

	public void OnProvidersExecuting(ApplicationModelProviderContext context) { }

	private static void ProcessCustomersController(string prefix, IEdmModel model, ControllerModel controllerModel)
	{
		foreach (ActionModel actionModel in controllerModel.Actions)
		{
			if (actionModel.ActionName == "Get")
			{
				// For simplicity, I only check the parameter count
				if (actionModel.Parameters.Count == 0)
				{
					ODataPathTemplate path = new ODataPathTemplate(new EntitySetCustomersSegment());
					actionModel.AddSelector("get", prefix, model, path);
				}
				else
				{
					ODataPathTemplate path = new ODataPathTemplate(
						new EntitySetCustomersSegment(),
						new EntitySetWithKeySegment());

					actionModel.AddSelector("get", prefix, model, path);
				}
			}
		}
	}

	private static void ProcessMetadata(string prefix, IEdmModel model, ControllerModel controllerModel)
	{
		foreach (ActionModel actionModel in controllerModel.Actions)
		{
			if (actionModel.ActionName == "GetMetadata")
			{
				ODataPathTemplate path = new ODataPathTemplate(MetadataSegmentTemplate.Instance);
				actionModel.AddSelector("get", prefix, model, path);
			}
			else if (actionModel.ActionName == "GetServiceDocument")
			{
				ODataPathTemplate path = new ODataPathTemplate();
				actionModel.AddSelector("get", prefix, model, path);
			}
		}
	}
}
