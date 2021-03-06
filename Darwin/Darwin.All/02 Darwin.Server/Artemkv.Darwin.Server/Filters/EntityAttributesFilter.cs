﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artemkv.Darwin.Server.Filters
{
	public class EntityAttributesFilter : ListFilter
	{
		public EntityAttributesFilter(Guid entityId)
		{
			if (entityId != Guid.Empty)
			{
				this.EntityId = entityId;
				this.Add(new ListFilterParameter("Entity.Id", EntityId));
			}
		}

		public Guid EntityId { get; private set; }
	}
}
