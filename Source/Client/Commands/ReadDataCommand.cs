namespace BlazorApp1.Client.Commands;

using System.Linq.Expressions;
using System.Reflection;

using BlazorApp1.Client.Data;
using BlazorApp1.Shared.Extensions;

using Blazorise;
using Blazorise.DataGrid;

using Microsoft.OData.Client;

public class ReadDataCommand
{
	private readonly ODataServiceContext context;
	private readonly ILogger<ReadDataCommand> logger;

	public ReadDataCommand(ODataServiceContext context, ILogger<ReadDataCommand> logger)
	{
		this.logger = logger;
		this.context = context;
	}

	public DataServiceQuery<T> Execute<T>(DataGridReadDataEventArgs<T> eventArgs, string path)
	{
		DataServiceQuery<T> query = this.context.CreateQuery<T>(path);

		switch (eventArgs.ReadDataMode)
		{
			case DataGridReadDataMode.Paging:
				query = (DataServiceQuery<T>)query.Skip((eventArgs.Page - 1) * eventArgs.PageSize).Take(eventArgs.PageSize);
				break;
			case DataGridReadDataMode.Virtualize:
				query = (DataServiceQuery<T>)query.Skip(eventArgs.VirtualizeOffset).Take(eventArgs.VirtualizeCount);
				break;
		}

		foreach (DataGridColumnInfo column in eventArgs.Columns
			.OrderBy((DataGridColumnInfo column) => column.SortIndex))
		{
			switch (column.SortDirection)
			{
				case SortDirection.Ascending:
					if (query is IOrderedQueryable<T> ascending)
						query = (DataServiceQuery<T>)ascending.ThenBy(column.SortField);
					else
						query = (DataServiceQuery<T>)query.OrderBy(column.SortField);
					break;
				case SortDirection.Descending:
					if (query is IOrderedQueryable<T> descending)
						query = (DataServiceQuery<T>)descending.ThenByDescending(column.SortField);
					else
						query = (DataServiceQuery<T>)query.OrderByDescending(column.SortField);
					break;
			}

			ParameterExpression parameterExpression = Expression.Parameter(typeof(T));

			switch (column.ColumnType)
			{
				case DataGridColumnType.Text when column.SearchValue is string s && !string.IsNullOrWhiteSpace(s):
					MethodInfo? method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
					if (method is null)
						break;
					query = (DataServiceQuery<T>)query
						.Where(Expression.Lambda<Func<T, bool>>(
							Expression.Call(
								Expression.Property(parameterExpression, column.Field),
								method,
								Expression.Constant(s, typeof(string))),
							parameterExpression));
					break;
				case DataGridColumnType.Numeric when column.SearchValue is int i:
					query = (DataServiceQuery<T>)query
						.Where(Expression.Lambda<Func<T, bool>>(
							Expression.Equal(parameterExpression, Expression.Constant(i, typeof(int))),
							parameterExpression));
					break;
				case DataGridColumnType.Check when column.SearchValue is bool b:
					query = (DataServiceQuery<T>)query
						.Where(Expression.Lambda<Func<T, bool>>(
							Expression.Equal(parameterExpression, Expression.Constant(b, typeof(bool))),
							parameterExpression));
					break;
			}
		}

		return query;
	}
}
