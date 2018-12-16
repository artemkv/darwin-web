using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using NLog;

namespace Artemkv.Darwin.Common
{
	/// <summary>
	/// Catches unhandled exceptions thrown in a controller except HttpResponseException and converts into '500 Internal Server Error' response.
	/// HttpResponseException is handled by MVC infrastracture and converted into '500 Internal Server Error' response automatically.
	/// Logs complete exception, but keeps only message of the exception in the response.
	/// </summary>
	public class HandleApiExceptionAttribute : ExceptionFilterAttribute
	{
		private static Logger SystemErrorLogger = LogManager.GetLogger(Constants.Erorr5XXLogName);

		public override void OnException(HttpActionExecutedContext actionExecutedContext)
		{
			base.OnException(actionExecutedContext);

			// Log the exception
			var exception = actionExecutedContext.Exception;
			string message = String.Format(
					"{0} {1} routed to '{2}' has thrown an unhandled exception",
					actionExecutedContext.Request.Method,
					actionExecutedContext.Request.RequestUri,
					actionExecutedContext.ActionContext.ControllerContext.RouteData.Values["controller"]);
			SystemErrorLogger.ErrorException(message, exception);

			// Convert into '500 Internal Server Error' inserting the RequestId
			var httpError = new HttpError(exception.Message);
			httpError[Constants.ErrorRequestIdProperty] = HttpContext.Current.Items[Constants.RequestIdKey];
			actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, httpError);
		}
	}
}
