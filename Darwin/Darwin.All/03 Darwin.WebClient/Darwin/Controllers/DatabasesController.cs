using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Artemkv.Darwin.Server;
using Artemkv.Darwin.Server.Filters;
using Artemkv.Darwin.Server.RESTHelpers;
using Artemkv.Darwin.Server.RESTModel;
using Artemkv.Darwin.Server.TransactionScripts;

namespace Darwin.Controllers
{
	public class DatabasesController : ApiController
	{
		/// <summary>
		/// Returns a list of all databases in the project
		/// Usage:
		///		- User select a current database in the drop-down - to retrieve the list of available databases
		/// Includes domain object data for:
		///		- Database
		/// Links:
		///		- www.darwin.com/project/{id}/database/{id} (relation: self)
		/// </summary>
		/// <param name="request">Request.</param>
		/// <param name="projectId">Project id.</param>
		/// <param name="pageSize">Page size.</param>
		/// <param name="pageNumber">Page number.</param>
		/// <returns>List of all databases in the project.</returns>
		public HttpResponseMessage Get(HttpRequestMessage request, string projectId, int pageSize = 10, int pageNumber = 0)
		{
			// Parse and validate input parameters
			Guid projectIdParsed = this.ParseAndValidateProjectId(request, projectId);

			this.ValidatePageSize(request, pageSize);
			this.ValidatePageNumber(request, pageNumber);

			// Retrieve objects
			var project = this.RetrieveProject(request, projectIdParsed);

			var getObjectList = new GetObjectList<Database>();
			var queryResult = getObjectList.Execute(ObjectListDataSource.ProjectDatabases, new ProjectDatabasesFilter(project.Id), pageSize, pageNumber);

			// Add links
			var linkBuilder = new LinkBuilder(Url);
			foreach (var database in queryResult.ResultSet)
			{
				database.AddLinks(linkBuilder);
			}

			// Wrap into the collection
			var collection = this.WrapQueryResultIntoCollection(queryResult);

			// Return the object
			return request.CreateResponse(HttpStatusCode.OK, collection);
		}
	}
}
