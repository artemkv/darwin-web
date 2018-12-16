using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Artemkv.Darwin.Data;
using Artemkv.Darwin.Server.DataSources;
using Artemkv.Darwin.Server.RESTModel;

namespace Artemkv.Darwin.Server.TransactionScripts
{
	public class GetTreeNodes
	{
		public QueryResult<TreeNode> Execute(string dataSource, Guid parentId, TreeNodePath path, int pageSize, int pageNumber)
		{
			QueryResult<TreeNode> nodes = null;

			var serviceLocator = ServiceLocator.GetActive();
			var dataProvider = serviceLocator.DataProvider;

			using (var session = dataProvider.OpenSession())
			{
				using (var transaction = session.BeginTransaction())
				{
					switch (dataSource)
					{
						case ObjectTreeDataSource.ProjectTreeView:
							nodes = new ProjectTreeViewDataSource().GetNodes(parentId, path, session, pageSize, pageNumber);
							break;
						default:
							throw new InvalidOperationException(String.Format("Unsupported data source: {0}", dataSource));
					}
				}
			}

			return nodes;
		}
	}
}
