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
    public class BaseEnumValuesController : ApiController
    {
		/// <summary>
		/// Returns a list of all values of the base enum
		/// Usage:
		///		- Not used (normally retrieved with the base enum)
		/// Includes domain object data for:
		///		- BaseEnumValue
		/// Links:
		///		- www.darwin.com/project/{id}/database/{id}/baseenum/{id}/value/{id} (relation: self)
		/// </summary>
		/// <param name="request">Request.</param>
		/// <param name="projectId">Project id.</param>
		/// <param name="databaseId">Database id.</param>
		/// <param name="baseEnumId">Base enum id.</param>
		/// <param name="pageSize">Page size.</param>
		/// <param name="pageNumber">Page number.</param>
		/// <returns>List of all values of the base enum.</returns>
		public HttpResponseMessage Get(HttpRequestMessage request, string projectId, string databaseId, string baseEnumId, int pageSize = 10, int pageNumber = 0)
		{
			// Parse and validate input parameters
			Guid projectIdParsed = this.ParseAndValidateProjectId(request, projectId);
			Guid databaseIdParsed = this.ParseAndValidateDatabaseId(request, databaseId);
			Guid baseEnumIdParsed = this.ParseAndValidateBaseEnumId(request, baseEnumId);

			this.ValidatePageSize(request, pageSize);
			this.ValidatePageNumber(request, pageNumber);

			// Retrieve objects
			var project = this.RetrieveProject(request, projectIdParsed);
			var database = this.RetrieveDatabase(request, databaseIdParsed, project);
			var baseEnum = this.RetrieveBaseEnum(request, baseEnumIdParsed, database);

			var getObjectList = new GetObjectList<BaseEnumValue>();
			var queryResult = getObjectList.Execute(ObjectListDataSource.BaseEnumValues, new BaseEnumValuesFilter(baseEnum.Id), pageSize, pageNumber);

			// Add links
			var linkBuilder = new LinkBuilder(Url);
			linkBuilder.ProjectId = project.Id;
			linkBuilder.DatabaseId = database.Id;
			foreach (var baseEnumValue in queryResult.ResultSet)
			{
				baseEnumValue.AddLinks(linkBuilder);
			}

			// Wrap into the collection
			var collection = this.WrapQueryResultIntoCollection(queryResult);

			// Return the object
			return request.CreateResponse(HttpStatusCode.OK, collection);
		}
	}
}
