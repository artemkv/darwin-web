using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Artemkv.Darwin.Server.Mapping;
using Artemkv.Darwin.Server.Validation;
using System.Runtime.Serialization;

using ERModel = Artemkv.Darwin.ERModel;
using Artemkv.Darwin.Server.RESTHelpers;

namespace Artemkv.Darwin.Server.RESTModel
{
	[DataContract(Name = "database", Namespace = "")]
	public class Database : PersistentObject
	{
		private readonly List<DataType> _dataTypes = new List<DataType>();
		private readonly List<BaseEnum> _baseEnums = new List<BaseEnum>();

		public Database()
		{
		}

		[DataMember(Name = "projectId")]
		[ParentObject(typeof(ERModel.Project))]
		public Guid ProjectId { get; set; }

		[DataMember(Name = "dbname")]
		[SimpleProperty]
		[RegExpValidationRule(ValidationPatterns.DbIdentifierValidationPattern, 
			ValidationPatterns.DbIdentifierValidationPatternDescription)]
		public string DBName { get; set; }

		[DataMember(Name = "connectionString")]
		[SimpleProperty]
		public string ConnectionString { get; set; }

		[DataMember(Name = "dataTypes")]
		[ObjectCollection(parentProperty: "Database")]
		public virtual List<DataType> DataTypes
		{
			get
			{
				return _dataTypes;
			}
		}

		[DataMember(Name = "baseEnums")]
		[ObjectCollection(parentProperty: "Database")]
		public virtual List<BaseEnum> BaseEnums
		{
			get
			{
				return _baseEnums;
			}
		}

		[IgnoreDataMember]
		public override Type PersistentType
		{
			get { return typeof(ERModel.Database); }
		}

		public override void AddLinks(LinkBuilder linkBuilder)
		{
			linkBuilder.ProjectId = this.ProjectId;
			linkBuilder.DatabaseId = this.Id;

			this.Links.Add(new Link()
			{
				Href = linkBuilder.DatabaseSelf(this.Id),
				Rel = LinkRelations.Self
			});

			this.Links.Add(new Link()
			{
				Href = linkBuilder.DatabaseDataTypes(),
				Rel = LinkRelations.DataTypes
			});
			this.Links.Add(new Link()
			{
				Href = linkBuilder.DatabaseBaseEnums(),
				Rel = LinkRelations.BaseEnums
			});
			this.Links.Add(new Link()
			{
				Href = linkBuilder.DatabaseEntities(),
				Rel = LinkRelations.Entities
			});
			this.Links.Add(new Link()
			{
				Href = linkBuilder.DatabaseDiagrams(),
				Rel = LinkRelations.Diagrams
			});

			foreach (var dataType in this.DataTypes)
			{
				dataType.AddLinks(linkBuilder);
			}
		}

		public override string ToString()
		{
			return DBName;
		}
	}
}
