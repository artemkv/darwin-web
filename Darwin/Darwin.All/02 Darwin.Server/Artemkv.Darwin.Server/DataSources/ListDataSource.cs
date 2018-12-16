using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using NHibernate;
using System.Reflection;
using Artemkv.Darwin.ERModel;
using Artemkv.Darwin.Data;

using RESTModel = Artemkv.Darwin.Server.RESTModel;
using ERModel = Artemkv.Darwin.ERModel;

namespace Artemkv.Darwin.Server.DataSources
{
	public class ListDataSource<T, R>
		where T : RESTModel.PersistentObject
		where R : PersistentObject
	{
		public virtual QueryResult<T> GetItems(ListFilter filter, ISession session, int pageSize, int pageNumber)
		{
			var predicate = BuildPredicate(filter);

			var serviceLocator = ServiceLocator.GetActive();
			var dataProvider = serviceLocator.DataProvider;

			var asm = new Assembler();
			var queryResult = dataProvider.GetObjectList<R>(predicate, session, pageSize, pageNumber);
			var items = from x in queryResult.ResultSet select asm.GetDTO(x) as T;
			return new QueryResult<T>(items.ToList(), queryResult.Count);
		}

		protected virtual Expression<Func<R, bool>> BuildPredicate(ListFilter filter)
		{
			// TODO: this is the generic way to apply the filter, datasources should probably be able to override this behavior to apply any kind of filtering.

			if (filter.Count == 0)
			{
				return x => true;
			}

			var arg = Expression.Parameter(typeof(R), "x");
			var expressionList = new List<Expression>();
			foreach (var filterParam in filter)
			{
				string[] props = filterParam.Property.Split('.');

				Expression expr = arg;
				Type type = typeof(R);
				foreach (string prop in props)
				{
					PropertyInfo propInfo = type.GetProperty(prop);
					expr = Expression.Property(expr, propInfo);
					type = propInfo.PropertyType;
				}

				expressionList.Add(Expression.Equal(expr, Expression.Constant(filterParam.Value)));
			}
			Expression combined = expressionList.Aggregate((l, r) => Expression.AndAlso(l, r));
			var lambda = Expression.Lambda<Func<R, bool>>(combined, arg);

			return lambda;
		}
	}
}
