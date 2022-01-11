namespace BlazorApp1.Client.Data;

using System;

using BlazorApp1.Shared.Models.v1;

using Microsoft.OData.Client;

public class ODataServiceContext : DataServiceContext
{
	public ODataServiceContext(Uri serviceRoot) : base(serviceRoot)
	{
		this.Characters = base.CreateQuery<Character>("Characters");
		this.Features = base.CreateQuery<Feature>("Features");
		this.CoreEffects = base.CreateQuery<CoreEffect>("CoreEffects");
		this.Effects = base.CreateQuery<Effect>("Effects");
		this.CharacterFeatures = base.CreateQuery<CharacterFeature>("CharacterFeatures");
	}

	public DataServiceQuery<Character> Characters { get; }
	public DataServiceQuery<Feature> Features { get; }
	public DataServiceQuery<CoreEffect> CoreEffects { get; }
	public DataServiceQuery<Effect> Effects { get; }

	public DataServiceQuery<CharacterFeature> CharacterFeatures { get; }
}
