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
	[DataContract(Name = "baseEnum", Namespace = "")]
	public class BaseEnum : PersistentObject
	{
		private readonly List<BaseEnumValue> _values = new List<BaseEnumValue>();

		public BaseEnum()
		{
		}

		[DataMember(Name = "databaseId")]
		[ParentObject(typeof(ERModel.Database))]
		public Guid DatabaseId { get; set; }

		[DataMember(Name = "baseEnumName")]
		[SimpleProperty]
		[RegExpValidationRule(ValidationPatterns.DbIdentifierValidationPattern, 
			ValidationPatterns.DbIdentifierValidationPatternDescription)]
		public string BaseEnumName { get; set; }

		[DataMember(Name = "values")]
		[ObjectCollection(parentProperty: "BaseEnum")]
		public virtual List<BaseEnumValue> Values
		{
			get
			{
				return _values;
			}
		}

		[IgnoreDataMember]
		public override Type PersistentType
		{
			get { return typeof(ERModel.BaseEnum); }
		}

		public override void AddLinks(LinkBuilder linkBuilder)
		{
			linkBuilder.DatabaseId = this.DatabaseId;
			linkBuilder.BaseEnumId = this.Id;

			this.Links.Add(
				new Link()
				{
					Href = linkBuilder.BaseEnumSelf(this.Id),
					Rel = LinkRelations.Self
				}
			);

			this.Links.Add(
				new Link()
				{
					Href = linkBuilder.BaseEnumValues(),
					Rel = LinkRelations.BaseEnumValues
				}
			);

			foreach (var baseEnumValue in this.Values)
			{
				baseEnumValue.AddLinks(linkBuilder);
			}
		}

		public override string ToString()
		{
			return BaseEnumName;
		}
	}
}
