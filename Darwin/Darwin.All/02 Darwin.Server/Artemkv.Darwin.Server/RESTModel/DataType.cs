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
	[DataContract(Name = "dataType", Namespace = "")]
	public class DataType : PersistentObject
	{
		public DataType()
		{
		}

		[DataMember(Name = "databaseId")]
		[ParentObject(typeof(ERModel.Database))]
		public Guid DatabaseId { get; set; }

		[DataMember(Name = "typeName")]
		[SimpleProperty]
		[RegExpValidationRule(ValidationPatterns.DbIdentifierValidationPattern, 
			ValidationPatterns.DbIdentifierValidationPatternDescription)]
		public string TypeName { get; set; }

		[DataMember(Name = "hasLength")]
		[SimpleProperty]
		public bool HasLength { get; set; }

		[DataMember(Name = "baseEnumId")]
		[ObjectProperty(typeof(ERModel.BaseEnum))]
		public Guid BaseEnumId { get; set; }

		[IgnoreDataMember]
		public override Type PersistentType
		{
			get { return typeof(ERModel.DataType); }
		}

		public override void AddLinks(LinkBuilder linkBuilder)
		{
			linkBuilder.DatabaseId = this.DatabaseId;

			this.Links.Add(
				new Link()
				{
					Href = linkBuilder.DataTypeSelf(this.Id),
					Rel = LinkRelations.Self
				}
			);

			if (this.BaseEnumId != Guid.Empty)
			{
				this.Links.Add(
					new Link()
					{
						Href = linkBuilder.DataTypeBaseEnum(this.BaseEnumId),
						Rel = LinkRelations.BaseEnum
					}
				);
			}
		}

		public override string ToString()
		{
			return TypeName;
		}
	}
}
