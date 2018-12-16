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
	[DataContract(Name = "relationItem", Namespace = "")]
	public class RelationItem : PersistentObject
	{
		public RelationItem()
		{
		}

		[DataMember(Name = "relationId")]
		[ParentObject(typeof(ERModel.Relation))]
		public Guid RelationId { get; set; }

		[DataMember(Name = "primaryAttributeId")]
		[ObjectProperty(typeof(ERModel.Attribute))]
		[NotNullValidationRule]
		public Guid PrimaryAttributeId { get; set; }

		[DataMember(Name = "foreignAttributeId")]
		[ObjectProperty(typeof(ERModel.Attribute))]
		[NotNullValidationRule]
		public Guid ForeignAttributeId { get; set; }

		[DataMember(Name = "primaryEntityName")]
		[CalculatedProperty("Relation.PrimaryEntity.EntityName")]
		public string PrimaryEntityName { get; set; }

		[DataMember(Name = "foreignEntityName")]
		[CalculatedProperty("Relation.ForeignEntity.EntityName")]
		public string ForeignEntityName { get; set; }

		[DataMember(Name = "primaryAttributeName")]
		[CalculatedProperty("PrimaryAttribute.AttributeName")]
		public string PrimaryAttributeName { get; set; }

		[DataMember(Name = "foreignAttributeName")]
		[CalculatedProperty("ForeignAttribute.AttributeName")]
		public string ForeignAttributeName { get; set; }

		[DataMember(Name = "foreignAttributeRequired")]
		[CalculatedProperty("ForeignAttribute.IsRequired")]
		public bool ForeignAttributeRequired { get; set; }

		[IgnoreDataMember]
		public override Type PersistentType
		{
			get { return typeof(ERModel.RelationItem); }
		}

		public override void AddLinks(LinkBuilder linkBuilder)
		{
			linkBuilder.RelationId = this.RelationId;

			this.Links.Add(
				new Link()
				{
					Href = linkBuilder.RelationItemSelf(this.Id),
					Rel = LinkRelations.Self
				}
			);

			this.Links.Add(
				new Link()
				{
					Href = linkBuilder.RelationItemPrimaryAttribute(this.PrimaryAttributeId),
					Rel = LinkRelations.PrimaryAttribute
				}
			);
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(ForeignEntityName);
			sb.Append(".");
			sb.Append(ForeignAttributeName);
			sb.Append(" >- ");
			sb.Append(PrimaryEntityName);
			sb.Append(".");
			sb.Append(PrimaryAttributeName);

			return sb.ToString();
		}
	}
}
