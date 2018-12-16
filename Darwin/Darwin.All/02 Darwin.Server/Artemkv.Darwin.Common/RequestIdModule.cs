using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Artemkv.Darwin.Common
{
	/// <summary>
	/// Generates a sequential request Id and stores it in HttpContext.Current.Items.
	/// </summary>
	public class RequestIdModule : IHttpModule
	{
		private static int CurrentRequestNumber;

		public void Init(HttpApplication context)
		{
			context.BeginRequest += OnBeginRequest;
		}

		void OnBeginRequest(object sender, EventArgs e)
		{
			HttpContext.Current.Items[Constants.RequestIdKey] = Guid.NewGuid().ToString("N");

			Interlocked.Increment(ref CurrentRequestNumber);
			HttpContext.Current.Items[Constants.RequestNoKey] = CurrentRequestNumber.ToString("D10");
		}

		public void Dispose()
		{
			// Do nothing
		}
	}
}
