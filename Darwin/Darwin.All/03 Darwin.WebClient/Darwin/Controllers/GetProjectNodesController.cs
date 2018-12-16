using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Artemkv.Darwin.Data;
using Artemkv.Darwin.Server;
using Artemkv.Darwin.Server.RESTHelpers;
using Artemkv.Darwin.Server.RESTModel;
using Artemkv.Darwin.Server.TransactionScripts;
using Artemkv.Darwin.Server.TreePaths.ProjectTreePath;

namespace Darwin.Controllers
{
    public class GetProjectNodesController : ApiController
	{
		#region Constants

		private const char IdSeparator = '~';

		#endregion Constants

		/// <summary>
		/// Retrieves the nodes of the current level of the project tree.
		/// </summary>
		/// <param name="request">Request.</param>
		/// <param name="parentIds">Parent ids.</param>
		/// <param name="path">Path elements.</param>
		/// <param name="pageSize">Page size.</param>
		/// <param name="pageNumber">Page number.</param>
		/// <returns>Nodes of the current level of the project tree.</returns>
		public HttpResponseMessage Post(HttpRequestMessage request, string parentIds, string path, int pageSize = 10, int pageNumber = 0)
		{
			// Parse and validate input parameters
			string[] parentIdsParsed = ParseAndValidateParentIds(request, parentIds);
			TreeNodePath pathParsed = ParseAndValidatePath(request, path);

			if (parentIdsParsed.Length != pathParsed.Length)
			{
				return request.CreateErrorResponse(HttpStatusCode.BadRequest,
					String.Format("Parent ids ('{0}') length ({1}) is not equal to path ('{2}') length ({3}).", parentIds, parentIdsParsed.Length, path, pathParsed.Length));
			}

			// Process path by retrieving the object for every path element with the corresponding id from id chain
			var linkBuilder = new LinkBuilder(Url);

			Project project = null;
			Database database = null;
			BaseEnum baseEnum = null;
			Entity entity = null;
			Relation relation = null;

			Guid lastId = Guid.Empty;

			int i = 0;
			foreach (var element in pathParsed)
			{
				lastId = this.ParseAndValidateId(request, parentIdsParsed[i]);

				switch (element)
				{
					case Element.Project:
						VerifyProjectId(request, parentIdsParsed[i], project);
						break;
					case Element.Database:
						database = RetrieveDatabase(request, linkBuilder, parentIdsParsed[i], project);
						break;
					case Element.Folder_Diagrams:
						RetrieveFolder(request, parentIdsParsed[i], database.Id);
						break;
					case Element.Folder_BaseEnums:
						RetrieveFolder(request, parentIdsParsed[i], database.Id);
						break;
					case Element.Folder_DataTypes:
						RetrieveFolder(request, parentIdsParsed[i], database.Id);
						break;
					case Element.Folder_Entities:
						RetrieveFolder(request, parentIdsParsed[i], database.Id);
						break;
					case Element.Diagram:
						throw new NotImplementedException(); // TODO: implement
					case Element.BaseEnum:
						baseEnum = RetrieveBaseEnum(request, linkBuilder, parentIdsParsed[i], database);
						break;
					case Element.Entity:
						entity = RetrieveEntity(request, linkBuilder, parentIdsParsed[i], database);
						break;
					case Element.Folder_EntityAttributes:
						RetrieveFolder(request, parentIdsParsed[i], entity.Id);
						break;
					case Element.Folder_EntityRelations:
						RetrieveFolder(request, parentIdsParsed[i], entity.Id);
						break;
					case Element.Relation:
						relation = RetrieveRelation(request, linkBuilder, parentIdsParsed[i], entity);
						break;
					default:
						if (element != TreeNodePath.Root)
						{
							return request.CreateErrorResponse(HttpStatusCode.BadRequest, 
								String.Format("Element '{0}' is not expected at position {1} of the path '{2}'.", element, i, path));
						}
						project = RetrieveProject(request, linkBuilder, parentIdsParsed[i]);
						break;
				}
				i++;
			}

			// Retrieve nodes
			var getTreeNodes = new GetTreeNodes();
			QueryResult<TreeNode> treeNodes = getTreeNodes.Execute(ObjectTreeDataSource.ProjectTreeView, lastId, pathParsed, pageSize, pageNumber);

			// Add links
			foreach (var node in treeNodes.ResultSet)
			{
				node.AddLinks(linkBuilder, parentIds + IdSeparator + node.ObjectId, pathParsed);
			}

			// Wrap into the collection
			var collection = this.WrapQueryResultIntoCollection<TreeNode>(treeNodes);

			// Return the object
			return request.CreateResponse(HttpStatusCode.OK, collection);
		}

		#region Private Methods

		private void VerifyProjectId(HttpRequestMessage request, string projectId, Project project)
		{
			Guid projectIdParsed = this.ParseAndValidateProjectId(request, projectId);

			if (project == null)
			{
				var response = request.CreateErrorResponse(HttpStatusCode.BadRequest, "Project is requested before the root node.");
				throw new HttpResponseException(response);
			}

			if (projectIdParsed != project.Id)
			{
				var response = request.CreateErrorResponse(HttpStatusCode.BadRequest,
					String.Format("Project id '{0}' is not the same as root id '{1}'.", projectIdParsed, project.Id));
				throw new HttpResponseException(response);
			}
		}

		private Project RetrieveProject(HttpRequestMessage request, LinkBuilder linkBuilder, string projectId)
		{
			Guid projectIdParsed = this.ParseAndValidateProjectId(request, projectId);

			linkBuilder.ProjectId = projectIdParsed;

			return this.RetrieveProject(request, projectIdParsed);
		}

		private Database RetrieveDatabase(HttpRequestMessage request, LinkBuilder linkBuilder, string databaseId, Project project)
		{
			Guid databaseIdParsed = this.ParseAndValidateDatabaseId(request, databaseId);

			if (project == null)
			{
				var response = request.CreateErrorResponse(HttpStatusCode.BadRequest, "Database is requested before the project.");
				throw new HttpResponseException(response);
			}

			var database = this.RetrieveDatabase(request, databaseIdParsed, project);

			linkBuilder.DatabaseId = databaseIdParsed;

			return database;
		}

		private BaseEnum RetrieveBaseEnum(HttpRequestMessage request, LinkBuilder linkBuilder, string baseEnumId, Database database)
		{
			Guid baseEnumIdParsed = this.ParseAndValidateBaseEnumId(request, baseEnumId);

			if (database == null)
			{
				var response = request.CreateErrorResponse(HttpStatusCode.BadRequest, "Base enum is requested before the database.");
				throw new HttpResponseException(response);
			}

			var baseEnum = this.RetrieveBaseEnum(request, baseEnumIdParsed, database);

			linkBuilder.BaseEnumId = baseEnumIdParsed;

			return baseEnum;
		}

		private Entity RetrieveEntity(HttpRequestMessage request, LinkBuilder linkBuilder, string entityId, Database database)
		{
			Guid entityIdParsed = this.ParseAndValidateEntityId(request, entityId);

			if (database == null)
			{
				var response = request.CreateErrorResponse(HttpStatusCode.BadRequest, "Entity is requested before the database.");
				throw new HttpResponseException(response);
			}

			var entity = this.RetrieveEntity(request, entityIdParsed, database);

			linkBuilder.EntityId = entityIdParsed;

			return entity;
		}

		private Relation RetrieveRelation(HttpRequestMessage request, LinkBuilder linkBuilder, string relationId, Entity entity)
		{
			Guid relationIdParsed = this.ParseAndValidateRelationId(request, relationId);

			if (entity == null)
			{
				var response = request.CreateErrorResponse(HttpStatusCode.BadRequest, "Relation is requested before the entity.");
				throw new HttpResponseException(response);
			}

			var relation = this.RetrieveRelation(request, relationIdParsed, entity);

			linkBuilder.RelationId = relationIdParsed;
			linkBuilder.PrimaryEntityId = entity.Id;

			return relation;
		}

		private void RetrieveFolder(HttpRequestMessage request, string id, Guid parentId)
		{
			Guid idParsed = this.ParseAndValidateId(request, id);

			if (idParsed != parentId)
			{
				var response = request.CreateErrorResponse(HttpStatusCode.BadRequest, "Grouping folder id is expected to be parent object id.");
				throw new HttpResponseException(response);
			}
		}

		private static string[] ParseAndValidateParentIds(HttpRequestMessage request, string parentIds)
		{
			if (String.IsNullOrWhiteSpace(parentIds))
			{
				var response = request.CreateErrorResponse(HttpStatusCode.BadRequest, "Parameter parentIds is not provided.");
				throw new HttpResponseException(response);
			}

			string[] parentIdsParsed = parentIds.Split(new char[1] { IdSeparator });
			return parentIdsParsed;
		}

		private static TreeNodePath ParseAndValidatePath(HttpRequestMessage request, string path)
		{
			TreeNodePath pathParsed = null;
			if (!TreeNodePath.TryParse(path, out pathParsed))
			{
				var response = request.CreateErrorResponse(HttpStatusCode.BadRequest, String.Format("Path '{0}' is not a valid path", path));
				throw new HttpResponseException(response);
			}
			return pathParsed;
		}

		#endregion Private Methods
	}
}
