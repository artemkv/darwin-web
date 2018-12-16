using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artemkv.Darwin.Server.Filters
{
	public class RelationsItemsFilter : ListFilter
	{
		public RelationsItemsFilter(Guid relationId)
		{
			if (relationId != Guid.Empty)
			{
				this.RelationId = relationId;
				this.Add(new ListFilterParameter("Relation.Id", RelationId));
			}
		}

		public Guid RelationId { get; private set; }
	}
}
