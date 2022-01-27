namespace BlazorApp1.Shared.Extensions;

using System.Linq.Expressions;

public static class QueryableExtensions
{
	public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(
		this IQueryable<TSource> source, string propertyName)
	{
		ParameterExpression parameterExpression = Expression.Parameter(typeof(TSource));
		return source.OrderBy(Expression.Lambda<Func<TSource, TKey>>(
			Expression.Property(parameterExpression, propertyName), parameterExpression.Yield()));
	}

	public static IOrderedQueryable<TSource> OrderByDescending<TSource, TKey>(
		this IQueryable<TSource> source, string propertyName)
	{
		ParameterExpression parameterExpression = Expression.Parameter(typeof(TSource));
		return source.OrderByDescending(Expression.Lambda<Func<TSource, TKey>>(
			Expression.Property(parameterExpression, propertyName), parameterExpression.Yield()));
	}

	public static IOrderedQueryable<TSource> ThenBy<TSource, TKey>(
		this IOrderedQueryable<TSource> source, string propertyName)
	{
		ParameterExpression parameterExpression = Expression.Parameter(typeof(TSource));
		return source.ThenBy(Expression.Lambda<Func<TSource, TKey>>(
			Expression.Property(parameterExpression, propertyName), parameterExpression.Yield()));
	}

	public static IOrderedQueryable<TSource> ThenByDescending<TSource, TKey>(
		this IOrderedQueryable<TSource> source, string propertyName)
	{
		ParameterExpression parameterExpression = Expression.Parameter(typeof(TSource));
		return source.ThenByDescending(Expression.Lambda<Func<TSource, TKey>>(
			Expression.Property(parameterExpression, propertyName), parameterExpression.Yield()));
	}

	public static IOrderedQueryable<TSource> OrderBy<TSource>(
		this IQueryable<TSource> source, string propertyName) => source.OrderBy<TSource, object>(propertyName);

	public static IOrderedQueryable<TSource> OrderByDescending<TSource>(
		this IQueryable<TSource> source, string propertyName) =>
			source.OrderByDescending<TSource, object>(propertyName);

	public static IOrderedQueryable<TSource> ThenBy<TSource>(
		this IOrderedQueryable<TSource> source, string propertyName) => source.ThenBy<TSource, object>(propertyName);

	public static IOrderedQueryable<TSource> ThenByDescending<TSource>(
		this IOrderedQueryable<TSource> source, string propertyName) =>
			source.ThenByDescending<TSource, object>(propertyName);
}
