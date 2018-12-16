using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Artemkv.Darwin.Server.RESTModel
{
	[DataContract(Name = "collection", Namespace = "")]
	public class Collection<T>
	{
		public Collection()
		{
		}

		[DataMember(Name = "items")]
		public IEnumerable<T> Items { get; set; }

		[DataMember(Name = "count")]
		public int Count { get; set; }
	}
}
