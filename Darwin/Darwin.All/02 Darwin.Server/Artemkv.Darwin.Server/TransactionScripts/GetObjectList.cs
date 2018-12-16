using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Artemkv.Darwin.Data;
using Artemkv.Darwin.Server.DataSources;
using Artemkv.Darwin.Server.RESTHelpers;

using RESTModel = Artemkv.Darwin.Server.RESTModel;
using ERModel = Artemkv.Darwin.ERModel;

namespace Artemkv.Darwin.Server.TransactionScripts
{
	public class GetObjectList<T> where T : RESTModel.PersistentObject
	{
		public QueryResult<T> Execute(string dataSource, ListFilter filter, int pageSize, int pageNumber)
		{
			QueryResult<T> queryResult = null;

			var serviceLocator = ServiceLocator.GetActive();
			var dataProvider = serviceLocator.DataProvider;

			using (var session = dataProvider.OpenSession())
			{
				using (var transaction = session.BeginTransaction())
				{
					switch (dataSource)
					{
						case ObjectListDataSource.Projects:
							queryResult = new ListDataSource<T, ERModel.Project>().GetItems(filter, session, pageSize, pageNumber);
							break;
						case ObjectListDataSource.ProjectDatabases:
							queryResult = new ListDataSource<T, ERModel.Database>().GetItems(filter, session, pageSize, pageNumber);
							break;
						case ObjectListDataSource.DatabaseBaseEnums:
							queryResult = new ListDataSource<T, ERModel.BaseEnum>().GetItems(filter, session, pageSize, pageNumber);
							break;
						case ObjectListDataSource.DatabaseDataTypes:
							queryResult = new ListDataSource<T, ERModel.DataType>().GetItems(filter, session, pageSize, pageNumber);
							break;
						case ObjectListDataSource.DatabaseEntities:
							queryResult = new ListDataSource<T, ERModel.Entity>().GetItems(filter, session, pageSize, pageNumber);
							break;
						case ObjectListDataSource.EntityAttributes:
							queryResult = new ListDataSource<T, ERModel.Attribute>().GetItems(filter, session, pageSize, pageNumber);
							break;
						case ObjectListDataSource.EntityRelations:
							queryResult = new ListDataSource<T, ERModel.Relation>().GetItems(filter, session, pageSize, pageNumber);
							break;
						case ObjectListDataSource.DiagramEntities:
							queryResult = new ListDataSource<T, ERModel.DiagramEntity>().GetItems(filter, session, pageSize, pageNumber);
							break;
						case ObjectListDataSource.RelationItems:
							queryResult = new ListDataSource<T, ERModel.RelationItem>().GetItems(filter, session, pageSize, pageNumber);
							break;
						case ObjectListDataSource.BaseEnumValues:
							queryResult = new ListDataSource<T, ERModel.BaseEnumValue>().GetItems(filter, session, pageSize, pageNumber);
							break;
						default:
							throw new InvalidOperationException(String.Format("Unsupported data source: {0}", dataSource));
					}
				}
			}
			return queryResult;
		}
	}
}
