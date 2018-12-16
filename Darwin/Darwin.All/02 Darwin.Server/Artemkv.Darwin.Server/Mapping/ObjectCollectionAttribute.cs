using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artemkv.Darwin.Server.Mapping
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public class ObjectCollectionAttribute : Attribute
	{
		public ObjectCollectionAttribute(
			string parentProperty,
			ObjectCollectionDeletionBehavior deletionBehavior = ObjectCollectionDeletionBehavior.Delete)
		{
			this.ParentProperty = parentProperty;
			this.DeletionBehavior = deletionBehavior;
		}

		public string ParentProperty { get; private set; }
		public ObjectCollectionDeletionBehavior DeletionBehavior { get; private set; }
	}
}
