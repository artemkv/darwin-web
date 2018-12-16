using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artemkv.Darwin.Server.Filters
{
	public class BaseEnumValuesFilter : ListFilter
	{
		public BaseEnumValuesFilter(Guid baseEnumId)
		{
			if (baseEnumId != Guid.Empty)
			{
				this.BaseEnumId = baseEnumId;
				this.Add(new ListFilterParameter("BaseEnum.Id", BaseEnumId));
			}
		}

		public Guid BaseEnumId { get; private set; }
	}
}
