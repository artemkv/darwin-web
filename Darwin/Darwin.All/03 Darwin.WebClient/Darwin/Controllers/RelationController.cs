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
    public class RelationController : ApiController
    {
		/// <summary>
		/// Returns the relation
		/// Usage:
		///		- User edits the properties of the relation - to retrieve the properties
		/// Includes domain object data for:
		///		- Relation
		///		- RelationItem
		/// Links:
		///		- www.darwin.com/project/{id}/database/{id}/entity/{id}/relation/{id} (relation: self)
		///		- www.darwin.com/project/{id}/database/{id}/entity/{id}/relation/{id}/items (relation: http://www.darwin.org/rels/relationitems)
		///		- www.darwin.com/project/{id}/database/{id}/entity/{id}/relation/{id}/item/{id} (relation: self)
		///		- www.darwin.com/project/{id}/database/{id}/entity/{id} (relation: http://www.darwin.org/rels/primaryentity)
		/// </summary>
		/// <param name="request">Request.</param>
		/// <param name="projectId">Project id.</param>
		/// <param name="databaseId">Database id.</param>
		/// <param name="entityId">Entity id.</param>
		/// <param name="id">Relation id.</param>
		/// <returns>The relation.</returns>
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
			var relation = this.RetrieveRelation(request, idParsed, entity);

			// Add links
			var linkBuilder = new LinkBuilder(Url);
			linkBuilder.ProjectId = project.Id;
			linkBuilder.DatabaseId = database.Id;
			relation.AddLinks(linkBuilder);

			// Return the object
			return request.CreateResponse(HttpStatusCode.OK, relation);
		}
	}
}
