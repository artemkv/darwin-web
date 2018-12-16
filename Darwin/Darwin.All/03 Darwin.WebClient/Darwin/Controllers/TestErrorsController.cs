using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Darwin.Controllers
{
    public class TestErrorsController : ApiController
    {
		/// <summary>
		/// This controller is used for testing error handling in api controller using filters.
		/// </summary>
		/// <param name="request">Request.</param>
		/// <param name="test">Test type.</param>
		/// <returns>Error.</returns>
		public HttpResponseMessage Get(HttpRequestMessage request, string test)
		{
			switch (test)
			{
				case "correctlyhandled":
					// CreateErrorResponse is used
					throw new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error message for correctly handled error."));
				case "incorrectlyhandled":
					// CreateResponse is used instead of CreateErrorResponse
					throw new HttpResponseException(request.CreateResponse(HttpStatusCode.BadRequest, "Error message for incorrectly handled error."));
				case "unhandled":
					// Original unwrapped exception from inner layers
					throw new InvalidOperationException("Error message for unhandled error.");
				default:
					return request.CreateErrorResponse(
						HttpStatusCode.BadRequest,
						String.Format("Unknown value '{0}' for parameter 'test'. Allowed values: 'correctlyhandled', 'incorrectlyhandled', 'unhandled'", test));
			};
		}
	}
}
