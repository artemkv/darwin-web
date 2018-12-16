using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Routing;

namespace Artemkv.Darwin.Server.RESTHelpers
{
	public class LinkBuilder
	{
		#region Class Members

		private UrlHelper _urlHelper;
		private Guid _projectId = Guid.Empty;
		private Guid _databaseId = Guid.Empty;
		private Guid _entityId = Guid.Empty;
		private Guid _relationId = Guid.Empty;
		private Guid _primaryEntityId = Guid.Empty;
		private Guid _baseEnumId = Guid.Empty;

		#endregion Class Members

		#region Constructor

		public LinkBuilder(UrlHelper urlHelper)
		{
			if (urlHelper == null)
				throw new ArgumentNullException("urlHelper");

			_urlHelper = urlHelper;
		}

		#endregion Constructor

		#region Public Properties

		public Guid ProjectId
		{
			get
			{
				if (_projectId == Guid.Empty)
					throw new InvalidOperationException("Cannot create links. Project id is not initialized.");

				return _projectId;
			}
			set
			{
				if (value == Guid.Empty)
					throw new ArgumentNullException("value");

				_projectId = value;
			}
		}

		public Guid DatabaseId
		{
			get
			{
				if (_databaseId == Guid.Empty)
					throw new InvalidOperationException("Cannot create links. Database id is not initialized.");

				return _databaseId;
			}
			set
			{
				if (value == Guid.Empty)
					throw new ArgumentNullException("value");

				_databaseId = value;
			}
		}

		public Guid EntityId
		{
			get
			{
				if (_entityId == Guid.Empty)
					throw new InvalidOperationException("Cannot create links. Entity id is not initialized.");

				return _entityId;
			}
			set
			{
				if (value == Guid.Empty)
					throw new ArgumentNullException("value");

				_entityId = value;
			}
		}

		public Guid RelationId
		{
			get
			{
				if (_relationId == Guid.Empty)
					throw new InvalidOperationException("Cannot create links. Relation id is not initialized.");

				return _relationId;
			}
			set
			{
				if (value == Guid.Empty)
					throw new ArgumentNullException("value");

				_relationId = value;
			}
		}

		public Guid PrimaryEntityId
		{
			get
			{
				if (_primaryEntityId == Guid.Empty)
					throw new InvalidOperationException("Cannot create links. Primary entity id is not initialized.");

				return _primaryEntityId;
			}
			set
			{
				if (value == Guid.Empty)
					throw new ArgumentNullException("value");

				_primaryEntityId = value;
			}
		}

		public Guid BaseEnumId
		{
			get
			{
				if (_baseEnumId == Guid.Empty)
					throw new InvalidOperationException("Cannot create links. Base enum id is not initialized.");

				return _baseEnumId;
			}
			set
			{
				if (value == Guid.Empty)
					throw new ArgumentNullException("value");

				_baseEnumId = value;
			}
		}

		#endregion Public Properties

		#region Public Methods

		#region Self-Links

		public string ProjectSelf(Guid id)
		{
			return _urlHelper.Link("project", new { id = id });
		}

		public string DatabaseSelf(Guid id)
		{
			return _urlHelper.Link("database", new { projectid = this.ProjectId, id = id });
		}

		public string DataTypeSelf(Guid id)
		{
			return _urlHelper.Link("datatype", new { projectid = this.ProjectId, databaseid = this.DatabaseId, id = id });
		}

		public string EntitySelf(Guid id)
		{
			return _urlHelper.Link("entity", new { projectid = this.ProjectId, databaseid = this.DatabaseId, id = id });
		}

		public string AttributeSelf(Guid id)
		{
			return _urlHelper.Link("attribute", new { projectid = this.ProjectId, databaseid = this.DatabaseId, entityId = this.EntityId, id = id });
		}

		public string RelationSelf(Guid id)
		{
			return _urlHelper.Link("relation", new { projectid = this.ProjectId, databaseid = this.DatabaseId, entityId = this.EntityId, id = id });
		}

		public string RelationItemSelf(Guid id)
		{
			return _urlHelper.Link("relationitem", new { projectid = this.ProjectId, databaseid = this.DatabaseId, entityId = this.EntityId, relationid = this.RelationId, id = id });
		}

		public string BaseEnumSelf(Guid id)
		{
			return _urlHelper.Link("baseenum", new { projectid = this.ProjectId, databaseid = this.DatabaseId, id = id });
		}

		public string BaseEnumValueSelf(Guid id)
		{
			return _urlHelper.Link("baseenumvalue", new { projectid = this.ProjectId, databaseid = this.DatabaseId, baseenumid = this.BaseEnumId, id = id });
		}

		#endregion Self-Links

		public string ProjectDatabases()
		{
			return _urlHelper.Link("databases", new { projectid = this.ProjectId });
		}

		public string ProjectNodes(string parentIds, string path)
		{
			return _urlHelper.Link("getprojectnodes", new { parentids = parentIds, path = path });
		}

		public string DataTypeBaseEnum(Guid id)
		{
			return _urlHelper.Link("baseenum", new { projectid = this.ProjectId, databaseid = this.DatabaseId, id = id });
		}

		public string DatabaseDataType(Guid id)
		{
			return _urlHelper.Link("datatype", new { projectid = this.ProjectId, databaseid = this.DatabaseId, id = id });
		}

		public string RelationPrimaryEntity(Guid id)
		{
			return _urlHelper.Link("entity", new { projectid = this.ProjectId, databaseid = this.DatabaseId, id = id });
		}

		public string RelationItemPrimaryAttribute(Guid id)
		{
			return _urlHelper.Link("attribute", new { projectid = this.ProjectId, databaseid = this.DatabaseId, entityId = this.EntityId, id = id });
		}

		public string DatabaseDataTypes()
		{
			return _urlHelper.Link("datatypes", new { projectid = this.ProjectId, databaseid = this.DatabaseId });
		}

		public string DatabaseBaseEnums()
		{
			return _urlHelper.Link("baseenums", new { projectid = this.ProjectId, databaseid = this.DatabaseId });
		}

		public string DatabaseEntities()
		{
			return _urlHelper.Link("entities", new { projectid = this.ProjectId, databaseid = this.DatabaseId });
		}

		public string DatabaseDiagrams()
		{
			return _urlHelper.Link("diagrams", new { projectid = this.ProjectId, databaseid = this.DatabaseId });
		}

		public string EntityAttributes()
		{
			return _urlHelper.Link("attributes", new { projectid = this.ProjectId, databaseid = this.DatabaseId, entityId = this.EntityId });
		}

		public string EntityRelations()
		{
			return _urlHelper.Link("relations", new { projectid = this.ProjectId, databaseid = this.DatabaseId, entityId = this.EntityId });
		}

		public string RelationItems()
		{
			return _urlHelper.Link("relationitems", new { projectid = this.ProjectId, databaseid = this.DatabaseId, entityId = this.EntityId, relationid = this.RelationId });
		}

		public string BaseEnumValues()
		{
			return _urlHelper.Link("baseenumvalues", new { projectid = this.ProjectId, databaseid = this.DatabaseId, baseenumid = this.BaseEnumId });
		}

		#endregion Public Methods
	}
}
