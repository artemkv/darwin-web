using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Artemkv.Darwin.Server;
using Artemkv.Darwin.Server.RESTHelpers;
using Artemkv.Darwin.Server.RESTModel;
using Artemkv.Darwin.Server.TransactionScripts;

namespace Darwin.Controllers
{
    public class ProjectController : ApiController
    {
		/// <summary>
		/// Returns the project 
		/// Usage:
		/// 	- Building the project tree view - to get the link to the getnodes controller
		/// 	- User edits the properties of the project - to retrieve the properties
		/// Includes domain object data for:
		/// 	- Project
		/// Links:
		/// 	- www.darwin.com/project/{id} (relation: self)
		/// 	- www.darwin.com/project/{id}/getnodes (relation: http://www.darwin.com/rels/getprojectnodes)
		/// 	- www.darwin.com/project/{id}/databases (relation: http://www.darwin.com/rels/databases)
		/// </summary>
		/// <param name="request">Request.</param>
		/// <param name="id">Project id.</param>
		/// <returns>The project.</returns>
		public HttpResponseMessage Get(HttpRequestMessage request, string id)
		{
			// Parse and validate input parameters
			Guid idParsed = this.ParseAndValidateId(request, id);

			// Retrieve objects
			var project = this.RetrieveProject(request, idParsed);

			// Add links
			var linkBuilder = new LinkBuilder(Url);
			project.AddLinks(linkBuilder);

			// Return the object
			return request.CreateResponse(HttpStatusCode.OK, project);
		}
    }
}
