using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NLog;
using NLog.LayoutRenderers;

namespace Artemkv.Darwin.Common
{
	[LayoutRenderer("RequestNo")]
	public class RequestNoLayoutRenderer : LayoutRenderer
	{
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(HttpContext.Current.Items[Constants.RequestNoKey]);
		}
	}
}
