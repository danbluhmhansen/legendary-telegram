namespace LegendaryTelegram.Server.Interfaces;

using Microsoft.OData.Edm;

public interface IODataModelProvider
{
  IEdmModel GetEdmModel(string apiVersion);
}

