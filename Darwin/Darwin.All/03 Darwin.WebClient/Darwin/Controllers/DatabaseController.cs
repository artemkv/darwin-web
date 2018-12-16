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
    public class DatabaseController : ApiController
    {
		/// <summary>
		/// Returns the database
		/// Usage:
		///		- User edits the properties of the database - to retrieve the properties
		/// Includes domain object data for:
		/// 	- Database
		/// 	- DataType
		/// Links:
		///		- www.darwin.com/project/{id}/database/{id} (relation: self)
		///		- www.darwin.com/project/{id}/database/{id}/datatypes (relation: http://www.darwin.org/rels/datatypes)
		///		- www.darwin.com/project/{id}/database/{id}/datatype/{id} (relation: self)
		///		- www.darwin.com/project/{id}/database/{id}/baseenums (relation: http://www.darwin.org/rels/baseenums)
		///		- www.darwin.com/project/{id}/database/{id}/entities (relation: http://www.darwin.org/rels/entities)
		///		- www.darwin.com/project/{id}/database/{id}/diagrams (relation: http://www.darwin.org/rels/diagrams)
		/// </summary>
		/// <param name="request">Request.</param>
		/// <param name="projectId">Project id.</param>
		/// <param name="id">Database id.</param>
		/// <returns>The database.</returns>
		public HttpResponseMessage Get(HttpRequestMessage request, string projectId, string id)
		{
			// Parse and validate input parameters
			Guid projectIdParsed = this.ParseAndValidateProjectId(request, projectId);
			Guid idParsed = this.ParseAndValidateId(request, id);

			// Retrieve objects
			var project = this.RetrieveProject(request, projectIdParsed);
			var database = this.RetrieveDatabase(request, idParsed, project);

			// Add links
			var linkBuilder = new LinkBuilder(Url);
			database.AddLinks(linkBuilder);

			// Return the object
			return request.CreateResponse(HttpStatusCode.OK, database);
		}
	}
}
