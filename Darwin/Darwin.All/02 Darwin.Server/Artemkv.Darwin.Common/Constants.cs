using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artemkv.Darwin.Common
{
	public class Constants
	{
		/// <summary>
		/// Key for request id (unique id generated per every web request)
		/// </summary>
		public static readonly string RequestIdKey = "Darwin.RequestId";
		
		/// <summary>
		/// Key for request number (sequential number increasing for every request)
		/// </summary>
		public static readonly string RequestNoKey = "Darwin.RequestNo";

		public static readonly string ErrorRequestIdProperty = "RequestId";

		public static readonly string Erorr4XXLogName = "Error.4xx";

		public static readonly string Erorr5XXLogName = "Error.5xx";

		public static readonly string PerformanceAPILogName = "Performance.API";
	}
}
