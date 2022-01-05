namespace BlazorApp1.Server;

using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Routing.Conventions;
using Microsoft.AspNetCore.OData.Routing.Template;
using Microsoft.OData.Edm;

/// <summary>
/// The convention for <see cref="IEdmEntitySet"/>.
/// Conventions:
/// GET ~/entityset
/// GET ~/entityset/$count
/// GET ~/entityset/cast
/// GET ~/entityset/cast/$count
/// POST ~/entityset
/// POST ~/entityset/cast
/// PATCH ~/entityset ==> Delta resource set patch
/// </summary>
public class CustomEntitySetRoutingConvention : IODataControllerActionConvention
{
	/// <inheritdoc />
	public virtual int Order => 100;

	/// <inheritdoc />
	public virtual bool AppliesToController(ODataControllerActionContext context)
	{
		if (context == null)
		{
			throw new ArgumentNullException(nameof(context));
		}

		return context.EntitySet != null;
	}

	/// <inheritdoc />
	public virtual bool AppliesToAction(ODataControllerActionContext context)
	{
		if (context == null)
		{
			throw new ArgumentNullException(nameof(context));
		}

		ActionModel action = context.Action;
		IEdmEntitySet entitySet = context.EntitySet;
		IEdmEntityType entityType = entitySet.EntityType();

		// if the action has key parameter, skip it.
		if (action.HasODataKeyParameter(entityType))
		{
			return false;
		}

		string actionName = action.ActionName;

		// 1. Without type case
		if (ProcessEntitySetAction(actionName, entitySet, null, context, action))
		{
			return true;
		}

		// 2. process the derived type (cast) by searching all derived types
		// GetFrom{EntityTypeName} or Get{EntitySet}From{EntityTypeName}
		int index = actionName.IndexOf("From", StringComparison.Ordinal);
		if (index == -1)
		{
			return false;
		}

		string castTypeName = actionName[(index + 4)..]; // + 4 means to skip the "From"

		if (castTypeName.Length == 0)
		{
			// Early return for the following cases:
			// - Get|Post|PatchFrom
			// - Get|Patch{EntitySet}From
			// - Post{EntityType}From
			return false;
		}

		IEdmStructuredType? castType = FindTypeInInheritance(entityType, context.Model, castTypeName);
		if (castType == null)
		{
			return false;
		}

		string actionPrefix = actionName[..index];
		return ProcessEntitySetAction(actionPrefix, entitySet, castType, context, action);
	}

	private bool ProcessEntitySetAction(string actionName, IEdmEntitySet entitySet, IEdmStructuredType? castType,
		ODataControllerActionContext context, ActionModel action)
	{
		if (actionName == "Get" || actionName == $"Get{entitySet.Name}")
		{
			IEdmCollectionType? castCollectionType = null;
			if (castType != null)
			{
				castCollectionType = ToCollection(castType, true);
			}

			IEdmCollectionType entityCollectionType = ToCollection(entitySet.EntityType(), true);

			// GET ~/Customers or GET ~/Customers/Ns.VipCustomer
			IList<ODataSegmentTemplate> segments = new List<ODataSegmentTemplate>
			{
				new EntitySetSegmentTemplate(entitySet)
			};

			if (castType != null)
			{
				segments.Add(new CastSegmentTemplate(castCollectionType, entityCollectionType, entitySet));
			}

			ODataPathTemplate template = new ODataPathTemplate(segments);
			action.AddSelector("Get", context.Prefix, context.Model, template, context.Options?.RouteOptions);

			// GET ~/Customers/$count or GET ~/Customers/Ns.VipCustomer/$count
			segments = new List<ODataSegmentTemplate>
			{
				new EntitySetSegmentTemplate(entitySet)
			};

			if (castType != null)
			{
				segments.Add(new CastSegmentTemplate(castCollectionType, entityCollectionType, entitySet));
			}

			segments.Add(CountSegmentTemplate.Instance);

			template = new ODataPathTemplate(segments);
			action.AddSelector("Get", context.Prefix, context.Model, template, context.Options?.RouteOptions);
			return true;
		}
		else if (actionName == "Post" || actionName == $"Post{entitySet.EntityType().Name}")
		{
			// POST ~/Customers
			IList<ODataSegmentTemplate> segments = new List<ODataSegmentTemplate>
			{
				new EntitySetSegmentTemplate(entitySet)
			};

			if (castType != null)
			{
				IEdmCollectionType castCollectionType = ToCollection(castType, true);
				IEdmCollectionType entityCollectionType = ToCollection(entitySet.EntityType(), true);
				segments.Add(new CastSegmentTemplate(castCollectionType, entityCollectionType, entitySet));
			}
			ODataPathTemplate template = new ODataPathTemplate(segments);
			action.AddSelector("Post", context.Prefix, context.Model, template, context.Options?.RouteOptions);
			return true;
		}
		else if (actionName == "Put" || actionName == $"Put{entitySet.EntityType().Name}")
		{
			// PUT ~/Customers
			IList<ODataSegmentTemplate> segments = new List<ODataSegmentTemplate>
			{
				new EntitySetSegmentTemplate(entitySet)
			};

			if (castType != null)
			{
				IEdmCollectionType castCollectionType = ToCollection(castType, true);
				IEdmCollectionType entityCollectionType = ToCollection(entitySet.EntityType(), true);
				segments.Add(new CastSegmentTemplate(castCollectionType, entityCollectionType, entitySet));
			}
			ODataPathTemplate template = new ODataPathTemplate(segments);
			action.AddSelector("Put", context.Prefix, context.Model, template, context.Options?.RouteOptions);
			return true;
		}
		else if (actionName == "Delete" || actionName == $"Delete{entitySet.EntityType().Name}")
		{
			// DELETE ~/Customers
			IList<ODataSegmentTemplate> segments = new List<ODataSegmentTemplate>
			{
				new EntitySetSegmentTemplate(entitySet)
			};

			if (castType != null)
			{
				IEdmCollectionType castCollectionType = ToCollection(castType, true);
				IEdmCollectionType entityCollectionType = ToCollection(entitySet.EntityType(), true);
				segments.Add(new CastSegmentTemplate(castCollectionType, entityCollectionType, entitySet));
			}
			ODataPathTemplate template = new ODataPathTemplate(segments);
			action.AddSelector("Delete", context.Prefix, context.Model, template, context.Options?.RouteOptions);
			return true;
		}
		else if (actionName == "Patch" || actionName == $"Patch{entitySet.Name}")
		{
			// PATCH ~/Patch  , ~/PatchCustomers
			IList<ODataSegmentTemplate> segments = new List<ODataSegmentTemplate>
			{
				new EntitySetSegmentTemplate(entitySet)
			};

			if (castType != null)
			{
				IEdmCollectionType castCollectionType = ToCollection(castType, true);
				IEdmCollectionType entityCollectionType = ToCollection(entitySet.EntityType(), true);
				segments.Add(new CastSegmentTemplate(castCollectionType, entityCollectionType, entitySet));
			}

			ODataPathTemplate template = new ODataPathTemplate(segments);
			action.AddSelector("Patch", context.Prefix, context.Model, template, context.Options?.RouteOptions);
			return true;
		}

		return false;
	}

	/// <summary>
	/// Find the given type in a structured type inheritance, include itself.
	/// </summary>
	/// <param name="structuralType">The starting structural type.</param>
	/// <param name="model">The Edm model.</param>
	/// <param name="typeName">The searching type name.</param>
	/// <returns>The found type.</returns>
	public IEdmStructuredType? FindTypeInInheritance(
		IEdmStructuredType structuralType, IEdmModel model, string typeName)
	{
		IEdmStructuredType baseType = structuralType;
		while (baseType != null)
		{
			if (GetName(baseType) == typeName)
			{
				return baseType;
			}

			baseType = baseType.BaseType;
		}

		return model.FindAllDerivedTypes(structuralType).FirstOrDefault(c => GetName(c) == typeName);
	}

	private static string GetName(IEdmStructuredType type) =>
		type is IEdmEntityType entityType ? entityType.Name : ((IEdmComplexType)type).Name;

	/// <summary>
	/// Converts the <see cref="IEdmType"/> to <see cref="IEdmCollectionType"/>.
	/// </summary>
	/// <param name="edmType">The given Edm type.</param>
	/// <param name="isNullable">Nullable or not.</param>
	/// <returns>The collection type.</returns>
	public IEdmCollectionType ToCollection(IEdmType edmType, bool isNullable)
	{
		if (edmType == null)
		{
			throw new ArgumentNullException(nameof(edmType));
		}

		return new EdmCollectionType(ToEdmTypeReference(edmType, isNullable));
	}

	/// <summary>
	/// Converts an Edm Type to Edm type reference.
	/// </summary>
	/// <param name="edmType">The Edm type.</param>
	/// <param name="isNullable">Nullable value.</param>
	/// <returns>The Edm type reference.</returns>
	public IEdmTypeReference ToEdmTypeReference(IEdmType edmType, bool isNullable)
	{
		switch (edmType.TypeKind)
		{
			case EdmTypeKind.Collection:
				return new EdmCollectionTypeReference((IEdmCollectionType)edmType);

			case EdmTypeKind.Complex:
				return new EdmComplexTypeReference((IEdmComplexType)edmType, isNullable);

			case EdmTypeKind.Entity:
				return new EdmEntityTypeReference((IEdmEntityType)edmType, isNullable);

			case EdmTypeKind.EntityReference:
				return new EdmEntityReferenceTypeReference((IEdmEntityReferenceType)edmType, isNullable);

			case EdmTypeKind.Enum:
				return new EdmEnumTypeReference((IEdmEnumType)edmType, isNullable);

			case EdmTypeKind.Primitive:
				return EdmCoreModel.Instance.GetPrimitive(((IEdmPrimitiveType)edmType).PrimitiveKind, isNullable);

			case EdmTypeKind.Path:
				return new EdmPathTypeReference((IEdmPathType)edmType, isNullable);

			case EdmTypeKind.TypeDefinition:
				return new EdmTypeDefinitionReference((IEdmTypeDefinition)edmType, isNullable);

			default:
				throw new NotSupportedException();
		}
	}
}
