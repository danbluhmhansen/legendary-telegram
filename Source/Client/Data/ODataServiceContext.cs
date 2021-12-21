namespace BlazorApp1.Client.Data;

using System;

using BlazorApp1.Shared.Models.v1;

using Microsoft.OData.Client;

public class ODataServiceContext : DataServiceContext
{
	public ODataServiceContext(Uri serviceRoot) : base(serviceRoot)
	{
		this.Characters = base.CreateQuery<Character>("Characters");
	}

	public DataServiceQuery<Character> Characters { get; }
}
