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
    public class EntitiesController : ApiController
    {
		/// <summary>
		/// Returns a list of all entities in the database
		/// Usage:
		///		- User selects an entity to relate to - to retrieve the list of available entities
		///		- User selects entities to be shown on the diagram - to retrieve the list of available entities
		/// Includes domain object data for:
		///		- Entity
		/// Links:
		///		- www.darwin.com/project/{id}/database/{id}/entity/{id} (relation: self)
		/// </summary>
		/// <param name="request">Request.</param>
		/// <param name="projectId">Project id.</param>
		/// <param name="databaseId">Database id.</param>
		/// <param name="pageSize">Page size.</param>
		/// <param name="pageNumber">Page number.</param>
		/// <returns>List of all entities in the database.</returns>
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

			var getObjectList = new GetObjectList<Entity>();
			var queryResult = getObjectList.Execute(ObjectListDataSource.DatabaseEntities, new DatabaseEntitiesFilter(database.Id), pageSize, pageNumber);

			// Add links
			var linkBuilder = new LinkBuilder(Url);
			linkBuilder.ProjectId = project.Id;
			foreach (var entity in queryResult.ResultSet)
			{
				entity.AddLinks(linkBuilder);
			}

			// Wrap into the collection
			var collection = this.WrapQueryResultIntoCollection(queryResult);

			// Return the object
			return request.CreateResponse(HttpStatusCode.OK, collection);
		}
	}
}
