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
	public class RelationItemsController : ApiController
	{
		/// <summary>
		/// Returns a list of all relation items of the relation
		/// Usage:
		///		- Not used (normally retrieved with the relation)
		/// Includes domain object data for:
		///		- RelationItem
		/// Links:
		/// 	- www.darwin.com/project/{id}/database/{id}/entity/{id}/relation/{id}/item/{id} (relation: self)
		/// </summary>
		/// <param name="request">Request.</param>
		/// <param name="projectId">Project id.</param>
		/// <param name="databaseId">Database id.</param>
		/// <param name="entityId">Entity id.</param>
		/// <param name="relationId">Relation id.</param>
		/// <param name="pageSize">Page size.</param>
		/// <param name="pageNumber">Page number.</param>
		/// <returns>List of all relation items of the relation.</returns>
		public HttpResponseMessage Get(HttpRequestMessage request, string projectId, string databaseId, string entityId, string relationId, int pageSize = 10, int pageNumber = 0)
		{
			// Parse and validate input parameters
			Guid projectIdParsed = this.ParseAndValidateProjectId(request, projectId);
			Guid databaseIdParsed = this.ParseAndValidateDatabaseId(request, databaseId);
			Guid entityIdParsed = this.ParseAndValidateEntityId(request, entityId);
			Guid relationIdParsed = this.ParseAndValidateRelationId(request, relationId);

			this.ValidatePageSize(request, pageSize);
			this.ValidatePageNumber(request, pageNumber);

			// Retrieve objects
			var project = this.RetrieveProject(request, projectIdParsed);
			var database = this.RetrieveDatabase(request, databaseIdParsed, project);
			var entity = this.RetrieveEntity(request, entityIdParsed, database);
			var relation = this.RetrieveRelation(request, relationIdParsed, entity);

			var getObjectList = new GetObjectList<RelationItem>();
			var queryResult = getObjectList.Execute(ObjectListDataSource.RelationItems, new RelationsItemsFilter(relation.Id), pageSize, pageNumber);

			// Add links
			var linkBuilder = new LinkBuilder(Url);
			linkBuilder.ProjectId = project.Id;
			linkBuilder.DatabaseId = database.Id;
			linkBuilder.EntityId = entity.Id;
			linkBuilder.PrimaryEntityId = relation.PrimaryEntityId;
			foreach (var relationItem in queryResult.ResultSet)
			{
				relationItem.AddLinks(linkBuilder);
			}

			// Wrap into the collection
			var collection = this.WrapQueryResultIntoCollection(queryResult);

			// Return the object
			return request.CreateResponse(HttpStatusCode.OK, collection);
		}
	}
}
