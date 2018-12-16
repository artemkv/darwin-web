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
using RESTModel = Artemkv.Darwin.Server.RESTModel;

namespace Darwin.Controllers
{
    public class AttributesController : ApiController
    {
		/// <summary>
		/// Returns a list of all attributes of the entity
		/// Usage:
		///		- Not used (normally retrieved with the entity)
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
		/// <param name="pageSize">Page size.</param>
		/// <param name="pageNumber">Page number.</param>
		/// <returns>List of all attributes of the entity</returns>
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

			var getObjectList = new GetObjectList<RESTModel.Attribute>();
			var queryResult = getObjectList.Execute(ObjectListDataSource.EntityAttributes, new EntityAttributesFilter(entity.Id), pageSize, pageNumber);

			// Add links
			var linkBuilder = new LinkBuilder(Url);
			linkBuilder.ProjectId = project.Id;
			linkBuilder.DatabaseId = database.Id;
			foreach (var attribute in queryResult.ResultSet)
			{
				attribute.AddLinks(linkBuilder);
			}

			// Wrap into the collection
			var collection = this.WrapQueryResultIntoCollection(queryResult);

			// Return the object
			return request.CreateResponse(HttpStatusCode.OK, collection);
		}
	}
}
