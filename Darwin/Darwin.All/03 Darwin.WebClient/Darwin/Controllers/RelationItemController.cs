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
	public class RelationItemController : ApiController
	{
		/// <summary>
		/// Returns the relation item
		/// Usage:
		///		- User edits the properties of the relation item - to retrieve the properties
		/// Includes domain object data for:
		///		- RelationItem
		/// Links:
		///		- www.darwin.com/project/{id}/database/{id}/entity/{id}/relation/{id}/item/{id} (relation: self)
		///		- www.darwin.com/project/{id}/database/{id}/entity/{id}/attribute/{id} (relation: http://www.darwin.org/rels/primaryattribute)
		/// </summary>
		/// <param name="request">Request.</param>
		/// <param name="projectId">Project id.</param>
		/// <param name="databaseId">Database id.</param>
		/// <param name="entityId">Entity id.</param>
		/// <param name="relationId">Relation id.</param>
		/// <param name="id">Relation item id.</param>
		/// <returns>The relation item</returns>
		public HttpResponseMessage Get(HttpRequestMessage request, string projectId, string databaseId, string entityId, string relationId, string id)
		{
			// Parse and validate input parameters
			Guid projectIdParsed = this.ParseAndValidateProjectId(request, projectId);
			Guid databaseIdParsed = this.ParseAndValidateDatabaseId(request, databaseId);
			Guid entityIdParsed = this.ParseAndValidateEntityId(request, entityId);
			Guid relationIdParsed = this.ParseAndValidateRelationId(request, relationId);
			Guid idParsed = this.ParseAndValidateId(request, id);

			// Retrieve objects
			var project = this.RetrieveProject(request, projectIdParsed);
			var database = this.RetrieveDatabase(request, databaseIdParsed, project);
			var entity = this.RetrieveEntity(request, entityIdParsed, database);
			var relation = this.RetrieveRelation(request, relationIdParsed, entity);
			var relationItem = this.RetrieveRelationItem(request, idParsed, relation);

			// Add links
			var linkBuilder = new LinkBuilder(Url);
			linkBuilder.ProjectId = project.Id;
			linkBuilder.DatabaseId = database.Id;
			linkBuilder.EntityId = entity.Id;
			linkBuilder.PrimaryEntityId = relation.PrimaryEntityId;
			relationItem.AddLinks(linkBuilder);

			// Return the object
			return request.CreateResponse(HttpStatusCode.OK, relationItem);
		}
	}
}
