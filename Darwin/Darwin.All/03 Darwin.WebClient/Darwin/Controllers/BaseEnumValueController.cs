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
	public class BaseEnumValueController : ApiController
	{
		/// <summary>
		/// Returns the base enum value
		/// Usage:
		///		- User edits the properties of the base enum value - to retrieve the properties
		/// Includes domain object data for:
		///		- BaseEnumValue
		/// Links:
		///		- www.darwin.com/project/{id}/database/{id}/baseenum/{id}/value/{id} (relation: self)
		/// </summary>
		/// <param name="request">Request.</param>
		/// <param name="projectId">Project id.</param>
		/// <param name="databaseId">Database id.</param>
		/// <param name="baseEnumId">Base enum id.</param>
		/// <param name="id">Base enum value id.</param>
		/// <returns>The base enum value.</returns>
		public HttpResponseMessage Get(HttpRequestMessage request, string projectId, string databaseId, string baseEnumId, string id)
		{
			// Parse and validate input parameters
			Guid projectIdParsed = this.ParseAndValidateProjectId(request, projectId);
			Guid databaseIdParsed = this.ParseAndValidateDatabaseId(request, databaseId);
			Guid baseEnumIdParsed = this.ParseAndValidateBaseEnumId(request, baseEnumId);
			Guid idParsed = this.ParseAndValidateId(request, id);

			// Retrieve objects
			var project = this.RetrieveProject(request, projectIdParsed);
			var database = this.RetrieveDatabase(request, databaseIdParsed, project);
			var baseEnum = this.RetrieveBaseEnum(request, baseEnumIdParsed, database);
			var baseEnumValue = this.RetrieveBaseEnumValue(request, idParsed, baseEnum);

			// Add links
			var linkBuilder = new LinkBuilder(Url);
			linkBuilder.ProjectId = project.Id;
			linkBuilder.DatabaseId = database.Id;
			baseEnumValue.AddLinks(linkBuilder);

			// Return the object
			return request.CreateResponse(HttpStatusCode.OK, baseEnumValue);
		}
	}
}
