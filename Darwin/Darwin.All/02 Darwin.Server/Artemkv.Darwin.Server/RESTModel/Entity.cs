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
	[DataContract(Name = "entity", Namespace = "")]
	public class Entity : PersistentObject
	{
		private readonly List<Attribute> _attributes = new List<Attribute>();

		public Entity()
		{
		}

		[DataMember(Name = "databaseId")]
		[ParentObject(typeof(ERModel.Database))]
		public Guid DatabaseId { get; set; }

		[DataMember(Name = "schemaName")]
		[SimpleProperty]
		[RegExpValidationRule(ValidationPatterns.DbIdentifierValidationPattern, 
			ValidationPatterns.DbIdentifierValidationPatternDescription)]
		public string SchemaName { get; set; }

		[DataMember(Name = "entityName")]
		[SimpleProperty]
		[RegExpValidationRule(ValidationPatterns.DbIdentifierValidationPattern, 
			ValidationPatterns.DbIdentifierValidationPatternDescription)]
		public string EntityName { get; set; }

		[DataMember(Name = "attributes")]
		[ObjectCollection(parentProperty: "Entity")]
		public virtual List<Attribute> Attributes
		{
			get
			{
				return _attributes;
			}
		}

		[DataMember(Name = "entitySchemaPrefixedName")]
		public string EntitySchemaPrefixedName
		{
			get
			{
				return SchemaName + "." + EntityName;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		[IgnoreDataMember]
		public override Type PersistentType
		{
			get { return typeof(ERModel.Entity); }
		}

		public override void AddLinks(LinkBuilder linkBuilder)
		{
			linkBuilder.DatabaseId = this.DatabaseId;
			linkBuilder.EntityId = this.Id;

			this.Links.Add(
				new Link()
				{
					Href = linkBuilder.EntitySelf(this.Id),
					Rel = LinkRelations.Self
				}
			);

			this.Links.Add(
				new Link()
				{
					Href = linkBuilder.EntityAttributes(),
					Rel = LinkRelations.Attributes
				}
			);
			this.Links.Add(
				new Link()
				{
					Href = linkBuilder.EntityRelations(),
					Rel = LinkRelations.Relations
				}
			);

			foreach (var attribute in this.Attributes)
			{
				attribute.AddLinks(linkBuilder);
			}
		}

		public override string ToString()
		{
			return EntitySchemaPrefixedName;
		}
	}
}
