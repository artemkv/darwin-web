using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Artemkv.Darwin.Server;
using Artemkv.Darwin.Server.RESTHelpers;
using Artemkv.Darwin.Server.RESTModel;
using Artemkv.Darwin.Server.TransactionScripts;

namespace Darwin.Controllers
{
    public class ProjectsController : ApiController
    {
		/// <summary>
		/// Returns a list of all projects.
		/// Usage: 
		///		- User selects a project to open - to return the list of all projects available to user
		///		- Well-known entry point
		/// Includes domain object data for:
		///		- Project
		///	Links:
		///		- www.darwin.com/project/{id} (relation: self)
		/// </summary>
		/// <param name="request">Request.</param>
		/// <param name="pageSize">Page size.</param>
		/// <param name="pageNumber">Page number.</param>
		/// <returns>List of all projects.</returns>
		public HttpResponseMessage Get(HttpRequestMessage request, int pageSize = 10, int pageNumber = 0)
		{
			// Parse and validate input parameters
			this.ValidatePageSize(request, pageSize);
			this.ValidatePageNumber(request, pageNumber);

			// Retrieve objects
			var getObjectList = new GetObjectList<Project>();
			var queryResult = getObjectList.Execute(ObjectListDataSource.Projects, ListFilter.Empty, pageSize, pageNumber);

			// Add links
			var linkBuilder = new LinkBuilder(Url);
			foreach (var project in queryResult.ResultSet)
			{
				project.AddLinks(linkBuilder);
			}

			// Wrap into the collection
			var collection = this.WrapQueryResultIntoCollection(queryResult);

			// Return the object
			return request.CreateResponse(HttpStatusCode.OK, collection);
		}
    }
}
