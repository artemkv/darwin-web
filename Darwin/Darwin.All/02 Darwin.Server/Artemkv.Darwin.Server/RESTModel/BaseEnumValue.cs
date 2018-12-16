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
	[DataContract(Name = "baseEnumValue", Namespace = "")]
	public class BaseEnumValue : PersistentObject
	{
		public BaseEnumValue()
		{
		}

		[DataMember(Name = "baseEnumId")]
		[ParentObject(typeof(ERModel.BaseEnum))]
		public Guid BaseEnumId { get; set; }

		[DataMember(Name = "name")]
		[SimpleProperty]
		[RegExpValidationRule(ValidationPatterns.DbIdentifierValidationPattern, 
			ValidationPatterns.DbIdentifierValidationPatternDescription)]
		public string Name { get; set; }

		[DataMember(Name = "value")]
		[SimpleProperty]
		public int Value { get; set; }

		[IgnoreDataMember]
		public override Type PersistentType
		{
			get { return typeof(ERModel.BaseEnumValue); }
		}

		public override void AddLinks(LinkBuilder linkBuilder)
		{
			linkBuilder.BaseEnumId = this.BaseEnumId;

			this.Links.Add(
				new Link()
				{
					Href = linkBuilder.BaseEnumValueSelf(this.Id),
					Rel = LinkRelations.Self
				}
			);
		}

		public override string ToString()
		{
			return String.Format("{0} ({1})", Name, Value);
		}
	}
}
