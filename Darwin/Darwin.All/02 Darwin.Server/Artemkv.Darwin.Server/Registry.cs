using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Artemkv.Darwin.Data;

namespace Artemkv.Darwin.Server
{
	/// <summary>
	/// Long-living objects storage.
	/// </summary>
	internal class Registry
	{
		public DataProvider DataProvider { get; set; }
	}
}
