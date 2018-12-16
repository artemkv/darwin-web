using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using NLog;

namespace Artemkv.Darwin.Common
{
	/// <summary>
	/// Profiles REST API.
	/// Logs api calls, return results, timings and errors.
	/// </summary>
	public class ProfileApiAttribute : ActionFilterAttribute
	{
		private static Logger Logger = LogManager.GetCurrentClassLogger();
		private static Logger UserErrorLogger = LogManager.GetLogger(Constants.Erorr4XXLogName);
		private static Logger SystemErrorLogger = LogManager.GetLogger(Constants.Erorr5XXLogName);
		private static Logger PerformanceLogger = LogManager.GetLogger(Constants.PerformanceAPILogName);

		private Stopwatch _timer;

		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			base.OnActionExecuting(actionContext);

			// Always log beginning of the call
			Logger.Info(
				"BEGIN: {0} {1} routed to '{2}'",
				actionContext.Request.Method,
				actionContext.Request.RequestUri,
				actionContext.ControllerContext.RouteData.Values["controller"]);

			_timer = Stopwatch.StartNew();
		}

		public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
		{
			base.OnActionExecuted(actionExecutedContext);

			// Always log timing of the call
			_timer.Stop();
			PerformanceLogger.Debug(
				"{0} {1} routed to '{2}' executed in {3}ms",
				actionExecutedContext.Request.Method,
				actionExecutedContext.Request.RequestUri,
				actionExecutedContext.ActionContext.ControllerContext.RouteData.Values["controller"],
				_timer.Elapsed.TotalMilliseconds);

			// Response will be available if controller action executed successfully or thrown HttpResponseException
			// In case of HttpResponseException, response will contain the status code
			if (actionExecutedContext.Response != null)
			{
				// Logs ending of the call, whatever response was generate
				Logger.Info(
					"END: {0} {1} routed to '{2}' returned {3} {4}",
					actionExecutedContext.Request.Method,
					actionExecutedContext.Request.RequestUri,
					actionExecutedContext.ActionContext.ControllerContext.RouteData.Values["controller"],
					(int)actionExecutedContext.Response.StatusCode,
					actionExecutedContext.Response.ReasonPhrase);

				if (!actionExecutedContext.Response.IsSuccessStatusCode)
				{
					// This will not take care of unhandled exception which is not HttpResponseException
					// To insert request id in that case we need another filter
					string responseContent = AppendRequestIdToErrorResponseContentAndExtract(actionExecutedContext);

					// Logs an error
					string message = String.Format(
							"{0} {1} routed to '{2}' returned {3} {4} \n{5}",
							actionExecutedContext.Request.Method,
							actionExecutedContext.Request.RequestUri,
							actionExecutedContext.ActionContext.ControllerContext.RouteData.Values["controller"],
							(int)actionExecutedContext.Response.StatusCode,
							actionExecutedContext.Response.ReasonPhrase,
							responseContent);

					if ((int)actionExecutedContext.Response.StatusCode >= 500)
					{
						SystemErrorLogger.Error(message);
					}
					else if ((int)actionExecutedContext.Response.StatusCode >= 400)
					{
						UserErrorLogger.Error(message);
					}
				}
			}
			else
			{
				// Logs ending of the call in case there was no response (unhandled exception thrown)
				Logger.Info(
					"END: {0} {1} routed to '{2}' did not return any response",
					actionExecutedContext.Request.Method,
					actionExecutedContext.Request.RequestUri,
					actionExecutedContext.ActionContext.ControllerContext.RouteData.Values["controller"]);
			}
		}

		private static string AppendRequestIdToErrorResponseContentAndExtract(HttpActionExecutedContext actionExecutedContext)
		{
			string responseContent = "UNRECOGNIZED CONTENT";
			var objectContent = actionExecutedContext.Response.Content as ObjectContent;
			if (objectContent != null)
			{
				var httpError = objectContent.Value as HttpError;
				if (httpError != null)
				{
					// Insert Request Id
					httpError[Constants.ErrorRequestIdProperty] = HttpContext.Current.Items[Constants.RequestIdKey];

					StringBuilder sb = new StringBuilder();
					sb.AppendLine("<httpError>");
					foreach (var key in httpError.Keys)
					{
						sb.AppendLine(String.Format("	<item key='{0}'>{1}</item>", key, httpError[key]));
					}
					sb.AppendLine("</httpError>");
					responseContent = sb.ToString();
				}
				else
				{
					Logger.Warn("Error is not handled correctly. Use CreateErrorResponse to return errors.");

					responseContent = objectContent.Value.ToString();

					// Replace content with the HttpError
					var httpErrorNew = new HttpError(responseContent);
					httpErrorNew[Constants.ErrorRequestIdProperty] = HttpContext.Current.Items[Constants.RequestIdKey];
					actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(actionExecutedContext.Response.StatusCode, httpErrorNew);
				}
			}
			return responseContent;
		}
	}
}
