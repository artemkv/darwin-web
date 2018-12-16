using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artemkv.Darwin.Server.Mapping
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public class SimplePropertyAttribute : Attribute
	{
		public SimplePropertyAttribute()
		{
		}
	}
}
