using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artemkv.Darwin.Data
{
	public class QueryResult<T>
	{
		public static QueryResult<T> Empty
		{
			get
			{
				return new QueryResult<T>(new List<T>(), 0);
			}
		}

		public QueryResult(List<T> resultSet, int count)
		{
			this.ResultSet = resultSet;
			this.Count = count;
		}

		public List<T> ResultSet { get; private set; }
		public int Count { get; private set; }
	}
}
