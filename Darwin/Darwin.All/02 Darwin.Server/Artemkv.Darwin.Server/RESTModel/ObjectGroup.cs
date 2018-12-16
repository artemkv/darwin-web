using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Artemkv.Darwin.Server.RESTModel
{
	/// <summary>
	/// Represents the group of objects in the tree.
	/// </summary>
	[DataContract(Name = "objectGroup", Namespace = "")]
	public class ObjectGroup
	{
		/// <summary>
		/// Creates an instance of <c>ObjectGroup</c> class.
		/// </summary>
		/// <param name="title">Object group title.</param>
		public ObjectGroup(string title)
		{
			this.Title = title;
		}

		/// <summary>
		/// Gets or sets the object group title.
		/// </summary>
		[DataMember(Name = "title")]
		public string Title { get; private set; }

		public override string ToString()
		{
			return Title;
		}
	}
}
