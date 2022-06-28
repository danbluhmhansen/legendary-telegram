namespace LegendaryTelegram.Server.Services;

using System;
using System.Collections.Generic;

using LegendaryTelegram.Server.Interfaces;
using LegendaryTelegram.Server.Models;

using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

public class ODataModelProvider : IODataModelProvider
{
  private readonly IDictionary<string, IEdmModel> models = new Dictionary<string, IEdmModel>();

  public IEdmModel GetEdmModel(string apiVersion)
  {
    if (this.models.TryGetValue(apiVersion, out IEdmModel model))
    {
      return model;
    }

    model = BuildEdmModel(apiVersion);
    this.models[apiVersion] = model;
    return model;
  }

  private static IEdmModel BuildEdmModel(string version)
  {
    return version switch
    {
      "1.0" => BuildV1Model(),
        _ => throw new NotSupportedException($"The input version '{version}' is not supported!"),
    };
  }

  private static IEdmModel BuildV1Model()
  {
    ODataConventionModelBuilder oDataBuilder = new();
    oDataBuilder.EntitySet<Character>("Characters");
    return oDataBuilder.GetEdmModel();
  }
}
