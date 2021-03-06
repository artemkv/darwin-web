﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artemkv.Darwin.Server.Filters
{
	public class DatabaseDataTypesFilter : ListFilter
	{
		public DatabaseDataTypesFilter(Guid databaseId)
		{
			if (databaseId != Guid.Empty)
			{
				this.DatabaseId = databaseId;
				this.Add(new ListFilterParameter("Database.Id", DatabaseId));
			}
		}

		public Guid DatabaseId { get; private set; }
	}
}
