using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Artemkv.Darwin.Server.RESTModel
{
	[DataContract(Name = "link", Namespace = "")]
	public class Link
	{
		[DataMember(Name = "rel")]
		public string Rel { get; set; }

		[DataMember(Name = "href")]
		public string Href { get; set; }
	}
}
