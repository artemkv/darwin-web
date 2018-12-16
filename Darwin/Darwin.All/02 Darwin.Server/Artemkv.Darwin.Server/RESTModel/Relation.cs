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
	[DataContract(Name = "relation", Namespace = "")]
	public class Relation : PersistentObject
	{
		private readonly List<RelationItem> _items = new List<RelationItem>();

		public Relation()
		{
		}

		[DataMember(Name = "relationName")]
		[SimpleProperty]
		[RegExpValidationRule(ValidationPatterns.DbIdentifierValidationPattern, 
			ValidationPatterns.DbIdentifierValidationPatternDescription)]
		public string RelationName { get; set; }

		[DataMember(Name = "oneToOne")]
		[SimpleProperty]
		public bool OneToOne { get; set; }

		[DataMember(Name = "atLeastOne")]
		[SimpleProperty]
		public bool AtLeastOne { get; set; }

		[DataMember(Name = "primaryEntityId")]
		[ObjectProperty(typeof(ERModel.Entity))]
		public Guid PrimaryEntityId { get; set; }

		[DataMember(Name = "primaryEntity")]
		[ObjectViewProperty(typeof(ERModel.Entity))]
		public Entity PrimaryEntity { get; set; }

		[DataMember(Name = "foreignEntityId")]
		[ObjectProperty(typeof(ERModel.Entity))]
		public Guid ForeignEntityId { get; set; }

		[DataMember(Name = "foreignEntity")]
		[ObjectViewProperty(typeof(ERModel.Entity))]
		public Entity ForeignEntity { get; set; }

		[DataMember(Name = "items")]
		[ObjectCollection(parentProperty: "Relation")]
		public virtual List<RelationItem> Items
		{
			get
			{
				return _items;
			}
		}

		[IgnoreDataMember]
		public override Type PersistentType
		{
			get { return typeof(ERModel.Relation); }
		}

		public override void AddLinks(LinkBuilder linkBuilder)
		{
			linkBuilder.EntityId = this.ForeignEntityId;
			linkBuilder.RelationId = this.Id;
			linkBuilder.PrimaryEntityId = this.PrimaryEntityId;

			this.Links.Add(
				new Link()
				{
					Href = linkBuilder.RelationSelf(this.Id),
					Rel = LinkRelations.Self
				}
			);

			this.Links.Add(
				new Link()
				{
					Href = linkBuilder.RelationPrimaryEntity(this.PrimaryEntityId),
					Rel = LinkRelations.PrimaryEntity
				}
			);

			this.Links.Add(
				new Link()
				{
					Href = linkBuilder.RelationItems(),
					Rel = LinkRelations.RelationItems
				}
			);

			foreach (var item in this.Items)
			{
				item.AddLinks(linkBuilder);
			}

			this.PrimaryEntity.AddLinks(linkBuilder);
			this.ForeignEntity.AddLinks(linkBuilder);
		}

		public override string ToString()
		{
			return RelationName;
		}
	}
}
