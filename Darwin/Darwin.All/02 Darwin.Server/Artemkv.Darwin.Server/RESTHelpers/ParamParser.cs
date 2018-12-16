using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artemkv.Darwin.Server.RESTHelpers
{
	public static class ParamParser
	{
		public static Guid ParseGuid(string idString)
		{
			Guid id = Guid.Empty;

			if (!String.IsNullOrWhiteSpace(idString))
			{
				Guid.TryParse(idString, out id);
			}

			// If id is not present, it stays empty.
			// If id is present, then at this point it is valid and parsed.
			return id;
		}
	}
}
