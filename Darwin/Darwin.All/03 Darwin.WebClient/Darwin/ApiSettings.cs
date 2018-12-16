using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Darwin
{
	public static class ApiSettings
	{
		private const int MaxPageSizeDefault = 100;
		private const int MaxPageNumberDefault = 10000;

		static ApiSettings()
		{
			int maxPageSize;
			if (Int32.TryParse(ConfigurationManager.AppSettings["MaxPageSize"], out maxPageSize))
			{
				MaxPageSize = maxPageSize;
			}
			else
			{
				MaxPageSize = MaxPageSizeDefault;
			}

			int maxPageNumber;
			if (Int32.TryParse(ConfigurationManager.AppSettings["MaxPageNumber"], out maxPageNumber))
			{
				MaxPageNumber = maxPageNumber;
			}
			else
			{
				MaxPageNumber = MaxPageNumberDefault;
			}
		}

		public static int MaxPageSize { get; set; }
		public static int MaxPageNumber { get; set; }
	}
}