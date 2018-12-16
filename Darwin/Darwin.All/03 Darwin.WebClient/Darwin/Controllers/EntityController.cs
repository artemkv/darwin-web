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
	public class EntityController : ApiController
	{
		/// <summary>
		/// Returns the entity
		/// Usage:
		///		- User edits the properties of the entity - to retrieve the properties
		/// Includes domain object data for:
		/// 	- Entity
		///		- Attribute
		/// Links:
		///		- www.darwin.com/project/{id}/database/{id}/entity/{id} (relation: self)
		///		- www.darwin.com/project/{id}/database/{id}/entity/{id}/attributes (relation: http://www.darwin.org/rels/attributes)
		///		- www.darwin.com/project/{id}/database/{id}/entity/{id}/attribute/{id} (relation: self)
		///		- www.darwin.com/project/{id}/database/{id}/entity/{id}/relations (relation: http://www.darwin.org/rels/relations)
		/// </summary>
		/// <param name="request">Request.</param>
		/// <param name="projectId">Project id.</param>
		/// <param name="databaseId">Database id.</param>
		/// <param name="id">Entity id.</param>
		/// <returns>The entity.</returns>
		public HttpResponseMessage Get(HttpRequestMessage request, string projectId, string databaseId, string id)
		{
			// Parse and validate input parameters
			Guid projectIdParsed = this.ParseAndValidateProjectId(request, projectId);
			Guid databaseIdParsed = this.ParseAndValidateDatabaseId(request, databaseId);
			Guid idParsed = this.ParseAndValidateId(request, id);

			// Retrieve objects
			var project = this.RetrieveProject(request, projectIdParsed);
			var database = this.RetrieveDatabase(request, databaseIdParsed, project);
			var entity = this.RetrieveEntity(request, idParsed, database);

			// Add links
			var linkBuilder = new LinkBuilder(Url);
			linkBuilder.ProjectId = project.Id;
			entity.AddLinks(linkBuilder);

			// Return the object
			return request.CreateResponse(HttpStatusCode.OK, entity);
		}
	}
}
