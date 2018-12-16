using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Artemkv.Darwin.Server.RESTHelpers;
using Artemkv.Darwin.Server.RESTModel;

namespace Darwin.Controllers
{
	public class BaseEnumController : ApiController
	{
		/// <summary>
		/// Returns the base enum
		/// Usage:
		///		- User edits the properties of the base enum - to retrieve the properties
		/// Includes domain object data for:
		///		- BaseEnum
		/// Links:
		///		- www.darwin.com/project/{id}/database/{id}/baseenum/{id} (relation: self)
		///		- www.darwin.com/project/{id}/database/{id}/baseenum/{id}/values (relation: http://www.darwin.org/rels/baseenumvalues)
		///		- www.darwin.com/project/{id}/database/{id}/baseenum/{id}/value/{id} (relation: self)
		/// </summary>
		/// <param name="request">Request.</param>
		/// <param name="projectId">Project id.</param>
		/// <param name="databaseId">Database id.</param>
		/// <param name="id">Base enum id.</param>
		/// <returns>The base enum.</returns>
		public HttpResponseMessage Get(HttpRequestMessage request, string projectId, string databaseId, string id)
		{
			// Parse and validate input parameters
			Guid projectIdParsed = this.ParseAndValidateProjectId(request, projectId);
			Guid databaseIdParsed = this.ParseAndValidateDatabaseId(request, databaseId);
			Guid idParsed = this.ParseAndValidateId(request, id);

			// Retrieve objects
			var project = this.RetrieveProject(request, projectIdParsed);
			var database = this.RetrieveDatabase(request, databaseIdParsed, project);
			var baseEnum = this.RetrieveBaseEnum(request, idParsed, database);

			// Add links
			var linkBuilder = new LinkBuilder(Url);
			linkBuilder.ProjectId = project.Id;
			baseEnum.AddLinks(linkBuilder);

			// Return the object
			return request.CreateResponse(HttpStatusCode.OK, baseEnum);
		}
	}
}
