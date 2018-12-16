using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Artemkv.Darwin.Server.RESTHelpers;

namespace Artemkv.Darwin.Server.RESTModel
{
	[DataContract(Name = "treeNode", Namespace = "")]
	public class TreeNode
	{
		#region Class Members

		private List<Link> _links = new List<Link>();

		#endregion Class Members

		#region .Ctors

		public TreeNode(object obj, Guid objectId, string objectType, string subPath, bool isLeaf = false)
		{
			if (obj == null)
				throw new ArgumentNullException("obj");
			if (subPath == null)
				throw new ArgumentNullException("subPath");

			if (obj.GetType() == typeof(Database))
			{
				this.Database = obj as Database;
				this.IconUrl = "Content/resources/Icons/Database16.ico";
			}
			else if (obj.GetType() == typeof(Project))
			{
				this.Project = obj as Project;
				this.IconUrl = "Content/resources/Icons/Project16.ico";
			}
			else if (obj.GetType() == typeof(RESTModel.Attribute))
			{
				this.Attribute = obj as RESTModel.Attribute;
				this.IconUrl = "Content/resources/Icons/Attribute16.ico";
			}
			else if (obj.GetType() == typeof(BaseEnum))
			{
				this.BaseEnum = obj as BaseEnum;
				this.IconUrl = "Content/resources/Icons/BaseEnum16.ico";
			}
			else if (obj.GetType() == typeof(BaseEnumValue))
			{
				this.BaseEnumValue = obj as BaseEnumValue;
				this.IconUrl = "Content/resources/Icons/BaseEnumValue16.ico";
			}
			else if (obj.GetType() == typeof(DataType))
			{
				this.DataType = obj as DataType;
				this.IconUrl = "Content/resources/Icons/DataType16.ico";
			}
			else if (obj.GetType() == typeof(Entity))
			{
				this.Entity = obj as Entity;
				this.IconUrl = "Content/resources/Icons/Table16.ico";
			}
			else if (obj.GetType() == typeof(ObjectGroup))
			{
				this.ObjectGroup = obj as ObjectGroup;
				this.IconUrl = "Content/resources/Icons/Folder16.ico";
			}
			else if (obj.GetType() == typeof(Relation))
			{
				this.Relation = obj as Relation;
				this.IconUrl = "Content/resources/Icons/Relation16.ico";
			}
			else if (obj.GetType() == typeof(RelationItem))
			{
				this.RelationItem = obj as RelationItem;
				this.IconUrl = "Content/resources/Icons/RelationItem16.ico";
			}

			this.ObjectId = objectId;
			this.ObjectType = objectType;
			this.Title = obj.ToString();
			this.SubPath = subPath;
			this.IsLeaf = isLeaf;
		}

		#endregion .Ctors

		#region Properties

		[DataMember(Name = "database", EmitDefaultValue = false)]
		public Database Database { get; private set; }

		[DataMember(Name = "project", EmitDefaultValue = false)]
		public Project Project { get; private set; }

		[DataMember(Name = "attribute", EmitDefaultValue = false)]
		public RESTModel.Attribute Attribute { get; private set; }

		[DataMember(Name = "baseEnum", EmitDefaultValue = false)]
		public BaseEnum BaseEnum { get; private set; }

		[DataMember(Name = "baseEnumValue", EmitDefaultValue = false)]
		public BaseEnumValue BaseEnumValue { get; private set; }

		[DataMember(Name = "dataType", EmitDefaultValue = false)]
		public DataType DataType { get; private set; }

		[DataMember(Name = "entity", EmitDefaultValue = false)]
		public Entity Entity { get; private set; }

		[DataMember(Name = "relation", EmitDefaultValue = false)]
		public Relation Relation { get; private set; }

		[DataMember(Name = "relationItem", EmitDefaultValue = false)]
		public RelationItem RelationItem { get; private set; }

		[DataMember(Name = "objectGroup", EmitDefaultValue = false)]
		public ObjectGroup ObjectGroup { get; private set; }

		[DataMember(Name = "objectId")]
		public Guid ObjectId { get; private set; }

		[DataMember(Name = "objectType")]
		public string ObjectType { get; private set; }

		[DataMember(Name = "title")]
		public string Title { get; private set; }

		[DataMember(Name = "subPath")]
		public string SubPath { get; private set; }

		[DataMember(Name = "isLeaf")]
		public bool IsLeaf { get; private set; }

		[DataMember(Name = "links")]
		public List<Link> Links
		{
			get
			{
				return _links;
			}
		}

		[DataMember(Name = "iconUrl")]
		public string IconUrl { get; private set; }

		#endregion Properties

		#region Public Methods

		public void AddLinks(LinkBuilder linkBuilder, string parentIds, TreeNodePath path)
		{
			if (!this.IsLeaf)
			{
				this.Links.Add(new Link()
				{
					Href = linkBuilder.ProjectNodes(parentIds, path.Then(this.SubPath)),
					Rel = LinkRelations.GetNodes
				});
			}

			if (Database != null)
			{
				Database.AddLinks(linkBuilder);
			}
			else if (Project != null)
			{
				Project.AddLinks(linkBuilder);
			}
			else if (Attribute != null)
			{
				Attribute.AddLinks(linkBuilder);
			}
			else if (BaseEnum != null)
			{
				BaseEnum.AddLinks(linkBuilder);
			}
			else if (BaseEnumValue != null)
			{
				BaseEnumValue.AddLinks(linkBuilder);
			}
			else if (DataType != null)
			{
				DataType.AddLinks(linkBuilder);
			}
			else if (Entity != null)
			{
				Entity.AddLinks(linkBuilder);
			}
			else if (Relation != null)
			{
				Relation.AddLinks(linkBuilder);
			}
			else if (RelationItem != null)
			{
				RelationItem.AddLinks(linkBuilder);
			}
		}

		#endregion Public Methods
	}
}
