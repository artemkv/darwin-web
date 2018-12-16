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
	public class BaseEnumsController : ApiController
	{
		/// <summary>
		/// Returns a list of all base enums in the database
		/// Usage:
		///		- User selects an available base enum for the data type - to retrieve the list of available base enums
		///	Includes domain object data for:
		///		- BaseEnum
		/// Links:
		///		- www.darwin.com/project/{id}/database/{id}/baseenum/{id} (relation: self)
		/// </summary>
		/// <param name="request">Request.</param>
		/// <param name="projectId">Project id.</param>
		/// <param name="databaseId">Database id.</param>
		/// <param name="pageSize">Page size.</param>
		/// <param name="pageNumber">Page number.</param>
		/// <returns>List of all base enums in the database.</returns>
		public HttpResponseMessage Get(HttpRequestMessage request, string projectId, string databaseId, int pageSize = 10, int pageNumber = 0)
		{
			// Parse and validate input parameters
			Guid projectIdParsed = this.ParseAndValidateProjectId(request, projectId);
			Guid databaseIdParsed = this.ParseAndValidateDatabaseId(request, databaseId);

			this.ValidatePageSize(request, pageSize);
			this.ValidatePageNumber(request, pageNumber);

			// Retrieve objects
			var project = this.RetrieveProject(request, projectIdParsed);
			var database = this.RetrieveDatabase(request, databaseIdParsed, project);

			var getObjectList = new GetObjectList<BaseEnum>();
			var queryResult = getObjectList.Execute(ObjectListDataSource.DatabaseBaseEnums, new DatabaseBaseEnumsFilter(database.Id), pageSize, pageNumber);

			// Add links
			var linkBuilder = new LinkBuilder(Url);
			linkBuilder.ProjectId = project.Id;
			foreach (var baseEnum in queryResult.ResultSet)
			{
				baseEnum.AddLinks(linkBuilder);
			}

			// Wrap into the collection
			var collection = this.WrapQueryResultIntoCollection(queryResult);

			// Return the object
			return request.CreateResponse(HttpStatusCode.OK, collection);
		}
	}
}
