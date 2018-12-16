using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Artemkv.Darwin.Server;
using Artemkv.Darwin.Server.RESTHelpers;
using Artemkv.Darwin.Server.RESTModel;
using Artemkv.Darwin.Server.TransactionScripts;

namespace Darwin.Controllers
{
	public class DataTypeController : ApiController
	{
		/// <summary>
		/// Returns the data type
		/// Usage:
		///		- User edits the properties of the data type - to retrieve the properties
		/// Includes domain object data for:
		///		- DataType
		/// Links:
		///		- www.darwin.com/project/{id}/database/{id}/datatype/{id} (relation: self)
		///		- www.darwin.com/project/{id}/database/{id}/baseenum/{id} (relation: http://www.darwin.com/rels/baseenum)
		/// </summary>
		/// <param name="request">Request.</param>
		/// <param name="projectId">Project id.</param>
		/// <param name="databaseId">Database id.</param>
		/// <param name="id">Data type id.</param>
		/// <returns>The data type.</returns>
		public HttpResponseMessage Get(HttpRequestMessage request, string projectId, string databaseId, string id)
		{
			// Parse and validate input parameters
			Guid projectIdParsed = this.ParseAndValidateProjectId(request, projectId);
			Guid databaseIdParsed = this.ParseAndValidateDatabaseId(request, databaseId);
			Guid idParsed = this.ParseAndValidateId(request, id);

			// Retrieve objects
			var project = this.RetrieveProject(request, projectIdParsed);
			var database = this.RetrieveDatabase(request, databaseIdParsed, project);
			var dataType = this.RetrieveDataType(request, idParsed, database);

			// Add links
			var linkBuilder = new LinkBuilder(Url);
			linkBuilder.ProjectId = project.Id;
			dataType.AddLinks(linkBuilder);

			// Return the object
			return request.CreateResponse(HttpStatusCode.OK, dataType);
		}
	}
}
