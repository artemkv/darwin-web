using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Artemkv.Darwin.Server.RESTHelpers;
using Artemkv.Darwin.Server.RESTModel;
using RESTModel = Artemkv.Darwin.Server.RESTModel;

namespace Darwin.Controllers
{
	public class AttributeController : ApiController
	{
		/// <summary>
		/// Returns the attribute
		/// Usage:
		///		- User edits the properties of the attribute - to retrieve the properties
		/// Includes domain object data for:
		///		- Attribute
		/// Links:
		///		- www.darwin.com/project/{id}/database/{id}/entity/{id}/attribute/{id} (relation: self)
		///		- www.darwin.com/project/{id}/database/{id}/datatype/{id} (relation: http://www.darwin.org/rels/datatype)
		/// </summary>
		/// <param name="request">Request.</param>
		/// <param name="projectId">Project id.</param>
		/// <param name="databaseId">Database id.</param>
		/// <param name="entityId">Entity id.</param>
		/// <param name="id">Attribute id.</param>
		/// <returns>The attribute.</returns>
		public HttpResponseMessage Get(HttpRequestMessage request, string projectId, string databaseId, string entityId, string id)
		{
			// Parse and validate input parameters
			Guid projectIdParsed = this.ParseAndValidateProjectId(request, projectId);
			Guid databaseIdParsed = this.ParseAndValidateDatabaseId(request, databaseId);
			Guid entityIdParsed = this.ParseAndValidateEntityId(request, entityId);
			Guid idParsed = this.ParseAndValidateId(request, id);

			// Retrieve objects
			var project = this.RetrieveProject(request, projectIdParsed);
			var database = this.RetrieveDatabase(request, databaseIdParsed, project);
			var entity = this.RetrieveEntity(request, entityIdParsed, database);
			var attribute = this.RetrieveAttribute(request, idParsed, entity);

			// Add links
			var linkBuilder = new LinkBuilder(Url);
			linkBuilder.ProjectId = project.Id;
			linkBuilder.DatabaseId = database.Id;
			attribute.AddLinks(linkBuilder);

			// Return the object
			return request.CreateResponse(HttpStatusCode.OK, attribute);
		}
	}
}
