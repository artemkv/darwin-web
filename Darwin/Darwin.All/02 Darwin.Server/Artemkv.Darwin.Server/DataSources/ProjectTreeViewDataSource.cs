using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Artemkv.Darwin.Server.RESTModel;
using Artemkv.Darwin.Server.TreePaths.ProjectTreePath;
using NHibernate;

using RESTModel = Artemkv.Darwin.Server.RESTModel;
using ERModel = Artemkv.Darwin.ERModel;
using Artemkv.Darwin.Data;

namespace Artemkv.Darwin.Server.DataSources
{
	/// <summary>
	/// The datasource for the project tree.
	/// </summary>
	public class ProjectTreeViewDataSource
	{
		public QueryResult<TreeNode> GetNodes(Guid parentId, TreeNodePath path, ISession session, int pageSize, int pageNumber)
		{
			var serviceLocator = ServiceLocator.GetActive();
			var dataProvider = serviceLocator.DataProvider;

			IEnumerable<TreeNode> nodes = null;
			int count; // Unassigned to make compiler verify that the value is assigned in every 'if' clause.

			var asm = new Assembler();
			if (path == TreeNodePath.Root)
			{
				var project = asm.GetDTO(dataProvider.GetObject<ERModel.Project>(parentId, session));
				if (project != null)
				{
					nodes = new List<TreeNode>() 
					{
						new TreeNode(project as RESTModel.Project, project.Id, ObjectType.Project, Element.Project)
					};
				}
				else
				{
					nodes = new List<TreeNode>();
				}
				count = nodes.Count();
			}
			else if (path == TreeNodePath.Root.Then(Element.Project))
			{
				var queryResult = dataProvider.GetObjectList<ERModel.Database>(x => x.Project.Id == parentId, session, pageSize, pageNumber);
				nodes = from x in queryResult.ResultSet
						select new TreeNode(asm.GetDTO(x) as RESTModel.Database, x.Id, ObjectType.Database, Element.Database);
				count = queryResult.Count;
			}
			else if (path == TreeNodePath.Root
								.Then(Element.Project)
								.Then(Element.Database))
			{
				nodes = new List<TreeNode>() 
				{
					new TreeNode (new ObjectGroup("Diagrams"), parentId, String.Empty, Element.Folder_Diagrams), 
					new TreeNode (new ObjectGroup("Enums"), parentId, String.Empty, Element.Folder_BaseEnums), 
					new TreeNode (new ObjectGroup("Data Types"), parentId, String.Empty, Element.Folder_DataTypes), 
					new TreeNode (new ObjectGroup("Entities"), parentId, String.Empty, Element.Folder_Entities)
				};
				count = nodes.Count();
			}
			else if (path == TreeNodePath.Root
								.Then(Element.Project)
								.Then(Element.Database)
								.Then(Element.Folder_BaseEnums))
			{
				var queryResult = dataProvider.GetObjectList<ERModel.BaseEnum>(x => x.Database.Id == parentId, session, pageSize, pageNumber);
				nodes = from x in queryResult.ResultSet
						select new TreeNode(asm.GetDTO(x), x.Id, ObjectType.BaseEnum, Element.BaseEnum, isLeaf: false);
				count = queryResult.Count;
			}
			else if (path == TreeNodePath.Root
								.Then(Element.Project)
								.Then(Element.Database)
								.Then(Element.Folder_BaseEnums)
								.Then(Element.BaseEnum))
			{
				var queryResult = dataProvider.GetObjectList<ERModel.BaseEnumValue>(x => x.BaseEnum.Id == parentId, session, pageSize, pageNumber);
				nodes = from x in queryResult.ResultSet
						select new TreeNode(asm.GetDTO(x), x.Id, ObjectType.BaseEnumValue, Element.BaseEnumValue, isLeaf: true);
				count = queryResult.Count;
			}
			else if (path == TreeNodePath.Root
								.Then(Element.Project)
								.Then(Element.Database)
								.Then(Element.Folder_DataTypes))
			{
				var queryResult = dataProvider.GetObjectList<ERModel.DataType>(x => x.Database.Id == parentId, session, pageSize, pageNumber);
				nodes = from x in queryResult.ResultSet
						select new TreeNode(asm.GetDTO(x), x.Id, ObjectType.DataType, Element.DataType, isLeaf: true);
				count = queryResult.Count;
			}
			else if (path == TreeNodePath.Root
								.Then(Element.Project)
								.Then(Element.Database)
								.Then(Element.Folder_Entities))
			{
				var queryResult = dataProvider.GetObjectList<ERModel.Entity>(x => x.Database.Id == parentId, session, pageSize, pageNumber);
				nodes = from x in queryResult.ResultSet
						select new TreeNode(asm.GetDTO(x), x.Id, ObjectType.Entity, Element.Entity);
				count = queryResult.Count;
			}
			else if (path == TreeNodePath.Root
								.Then(Element.Project)
								.Then(Element.Database)
								.Then(Element.Folder_Entities)
								.Then(Element.Entity))
			{
				nodes = new List<TreeNode>() 
				{
					new TreeNode (new ObjectGroup("Attributes"), parentId, String.Empty, Element.Folder_EntityAttributes), 
					new TreeNode (new ObjectGroup("Relations"), parentId, String.Empty, Element.Folder_EntityRelations)
				};
				count = nodes.Count();
			}
			else if (path == TreeNodePath.Root
								.Then(Element.Project)
								.Then(Element.Database)
								.Then(Element.Folder_Entities)
								.Then(Element.Entity)
								.Then(Element.Folder_EntityAttributes))
			{
				var queryResult = dataProvider.GetObjectList<ERModel.Attribute>(x => x.Entity.Id == parentId, session, pageSize, pageNumber);
				nodes = from x in queryResult.ResultSet
						select new TreeNode(asm.GetDTO(x), x.Id, ObjectType.Attribute, Element.Attribute, isLeaf: true);
				count = queryResult.Count;
			}
			else if (path == TreeNodePath.Root
								.Then(Element.Project)
								.Then(Element.Database)
								.Then(Element.Folder_Entities)
								.Then(Element.Entity)
								.Then(Element.Folder_EntityRelations))
			{
				var queryResult = dataProvider.GetObjectList<ERModel.Relation>(x => x.ForeignEntity.Id == parentId, session, pageSize, pageNumber);
				nodes = from x in queryResult.ResultSet
						select new TreeNode(asm.GetDTO(x), x.Id, ObjectType.Relation, Element.Relation);
				count = queryResult.Count;
			}
			else if (path == TreeNodePath.Root
								.Then(Element.Project)
								.Then(Element.Database)
								.Then(Element.Folder_Entities)
								.Then(Element.Entity)
								.Then(Element.Folder_EntityRelations)
								.Then(Element.Relation))
			{
				var queryResult = dataProvider.GetObjectList<ERModel.RelationItem>(x => x.Relation.Id == parentId, session, pageSize, pageNumber);
				nodes = from x in queryResult.ResultSet
						select new TreeNode(asm.GetDTO(x), x.Id, ObjectType.RelationItem, Element.RelationItem, isLeaf: true);
				count = queryResult.Count;
			}
			else if (path == TreeNodePath.Root
								.Then(Element.Project)
								.Then(Element.Database)
								.Then(Element.Folder_Diagrams))
			{
				var queryResult = dataProvider.GetObjectList<ERModel.Diagram>(x => x.Database.Id == parentId, session, pageSize, pageNumber);
				nodes = from x in queryResult.ResultSet
						select new TreeNode(asm.GetDTO(x), x.Id, ObjectType.Diagram, Element.Diagram);
				count = queryResult.Count;
			}
			else if (path == TreeNodePath.Root
								.Then(Element.Project)
								.Then(Element.Database)
								.Then(Element.Folder_Diagrams)
								.Then(Element.Diagram))
			{
				var queryResult = dataProvider.GetObjectList<ERModel.DiagramEntity>(x => x.Diagram.Id == parentId, session, pageSize, pageNumber);
				nodes = from x in queryResult.ResultSet
						select new TreeNode(asm.GetDTO(x), x.Id, ObjectType.DiagramEntity, Element.DiagramEntity, isLeaf: true);
				count = queryResult.Count;
			}
			else
			{
				throw new InvalidOperationException(String.Format("Invalid path: {0}", path));
			}
			return new QueryResult<TreeNode>(nodes.ToList<TreeNode>(), count);
		}
	}
}
