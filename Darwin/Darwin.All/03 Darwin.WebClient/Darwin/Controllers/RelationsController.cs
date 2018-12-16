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
	public class RelationsController : ApiController
	{
		/// <summary>
		/// Returns a list of all relations of the entity
		/// Usage:
		///		- Not used (normally retrieved from the tree)
		/// Includes domain object data for:
		///		- Relation
		/// Links:
		///		- www.darwin.com/project/{id}/database/{id}/entity/{id}/relation/{id} (relation: self)
		/// </summary>
		/// <param name="request">Request.</param>
		/// <param name="projectId">Project id.</param>
		/// <param name="databaseId">Database id.</param>
		/// <param name="entityId">Entity id.</param>
		/// <param name="pageSize">Page size.</param>
		/// <param name="pageNumber">Page number.</param>
		/// <returns>List of all relations of the entity.</returns>
		public HttpResponseMessage Get(HttpRequestMessage request, string projectId, string databaseId, string entityId, int pageSize = 10, int pageNumber = 0)
		{
			// Parse and validate input parameters
			Guid projectIdParsed = this.ParseAndValidateProjectId(request, projectId);
			Guid databaseIdParsed = this.ParseAndValidateDatabaseId(request, databaseId);
			Guid entityIdParsed = this.ParseAndValidateEntityId(request, entityId);

			this.ValidatePageSize(request, pageSize);
			this.ValidatePageNumber(request, pageNumber);

			// Retrieve objects
			var project = this.RetrieveProject(request, projectIdParsed);
			var database = this.RetrieveDatabase(request, databaseIdParsed, project);
			var entity = this.RetrieveEntity(request, entityIdParsed, database);

			var getObjectList = new GetObjectList<Relation>();
			var queryResult = getObjectList.Execute(ObjectListDataSource.EntityRelations, new EntityRelationsFilter(entity.Id), pageSize, pageNumber);

			// Add links
			var linkBuilder = new LinkBuilder(Url);
			linkBuilder.ProjectId = project.Id;
			linkBuilder.DatabaseId = database.Id;
			foreach (var relation in queryResult.ResultSet)
			{
				relation.AddLinks(linkBuilder);
			}

			// Wrap into the collection
			var collection = this.WrapQueryResultIntoCollection(queryResult);

			// Return the object
			return request.CreateResponse(HttpStatusCode.OK, collection);
		}
	}
}
