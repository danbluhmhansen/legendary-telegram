namespace BlazorApp1.Client.Extensions;

using Microsoft.OData.Client;

/// <Summary>OData Client query extensions.</Summary>
public static class ODataClientQueryExtensions
{
	/**
	* <summary>Convert the queryable to DataServiceQuery and execute it.</summary>
	* <param name="queryable">The OData queryable.</param>
	* <param name="cancellationToken"></param>
	* <typeparam name="TElement">The entity type.</typeparam>
	* <returns>The OData query result.</returns>
	*/
	public static async Task<QueryOperationResponse<TElement>> ExecuteAsync<TElement>(
		this IQueryable<TElement> queryable, CancellationToken cancellationToken = default) =>
		(QueryOperationResponse<TElement>)await ((DataServiceQuery<TElement>)queryable)
			.ExecuteAsync(cancellationToken).ConfigureAwait(false);
}
