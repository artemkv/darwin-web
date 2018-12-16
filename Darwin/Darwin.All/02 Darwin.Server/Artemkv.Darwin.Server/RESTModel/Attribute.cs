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
	[DataContract(Name = "attribute", Namespace = "")]
	public class Attribute : PersistentObject
	{
		public Attribute()
		{
		}

		[DataMember(Name = "entityId")]
		[ParentObject(typeof(ERModel.Entity))]
		public Guid EntityId { get; set; }

		[DataMember(Name = "attributeName")]
		[SimpleProperty]
		[RegExpValidationRule(ValidationPatterns.DbIdentifierValidationPattern, 
			ValidationPatterns.DbIdentifierValidationPatternDescription)]
		public string AttributeName { get; set; }

		[DataMember(Name = "dataTypeId")]
		[ObjectProperty(typeof(ERModel.DataType))]
		[NotNullValidationRule]
		public Guid DataTypeId { get; set; }

		[DataMember(Name = "length")]
		[SimpleProperty]
		public int Length { get; set; }

		[DataMember(Name = "isRequired")]
		[SimpleProperty]
		public bool IsRequired { get; set; }

		[DataMember(Name = "isPrimaryKey")]
		[SimpleProperty]
		public bool IsPrimaryKey { get; set; }

		[IgnoreDataMember]
		public override Type PersistentType
		{
			get { return typeof(ERModel.Attribute); }
		}

		public override void AddLinks(LinkBuilder linkBuilder)
		{
			linkBuilder.EntityId = this.EntityId;

			this.Links.Add(
				new Link()
				{
					Href = linkBuilder.AttributeSelf(this.Id),
					Rel = LinkRelations.Self
				}
			);

			if (this.DataTypeId != Guid.Empty)
			{
				this.Links.Add(
					new Link()
					{
						Href = linkBuilder.DatabaseDataType(this.DataTypeId),
						Rel = LinkRelations.DataType
					}
				);
			}
		}

		public override string ToString()
		{
			return AttributeName;
		}
	}
}
