using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;
using Artemkv.Darwin.Server.Validation;
using Artemkv.Darwin.Server.Mapping;

using ERModel = Artemkv.Darwin.ERModel;
using Artemkv.Darwin.Server.RESTHelpers;

namespace Artemkv.Darwin.Server.RESTModel
{
	[DataContract(Name = "__persistentObject", Namespace = "")]
	public abstract class PersistentObject
	{
		private List<Link> _links = new List<Link>();

		public PersistentObject()
		{
			this.Id = Guid.NewGuid();
		}

		[IgnoreDataMember]
		public abstract Type PersistentType { get; }

		[DataMember(Name = "id")]
		public Guid Id { get; set; }

		[DataMember(Name = "ts")]
		[SimpleProperty]
		public virtual Byte[] Ts { get; set; }

		[DataMember(Name = "links")]
		public List<Link> Links
		{
			get
			{
				return _links;
			}
		}

		public abstract void AddLinks(LinkBuilder linkBuilder);
	}
}
