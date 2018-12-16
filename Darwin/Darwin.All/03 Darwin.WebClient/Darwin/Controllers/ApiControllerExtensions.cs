using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Artemkv.Darwin.Data;
using Artemkv.Darwin.Server;
using Artemkv.Darwin.Server.RESTHelpers;
using Artemkv.Darwin.Server.RESTModel;
using Artemkv.Darwin.Server.TransactionScripts;

using RESTModel = Artemkv.Darwin.Server.RESTModel;

namespace Darwin.Controllers
{
	public static class ApiControllerExtensions
	{
		public static Collection<T> WrapQueryResultIntoCollection<T>(this ApiController apiController, QueryResult<T> queryResult)
		{
			var collection = new Collection<T>()
			{
				Items = queryResult.ResultSet,
				Count = queryResult.Count
			};

			return collection;
		}

		#region Validation Methods

		public static void ValidatePageSize(this ApiController apiController, HttpRequestMessage request, int pageSize)
		{
			if (pageSize < 0 || pageSize > ApiSettings.MaxPageSize)
			{
				var response = request.CreateErrorResponse(
					HttpStatusCode.BadRequest,
					String.Format("Page size '{0}' is invalid. Page size is expected to be in range 0..{1}", pageSize, ApiSettings.MaxPageSize));
				throw new HttpResponseException(response);
			}
		}

		public static void ValidatePageNumber(this ApiController apiController, HttpRequestMessage request, int pageNumber)
		{
			if (pageNumber < 0 || pageNumber > ApiSettings.MaxPageNumber)
			{
				var response = request.CreateErrorResponse(
					HttpStatusCode.BadRequest,
					String.Format("Page number '{0}' is invalid. Page number is expected to be in range 0..{1}", pageNumber, ApiSettings.MaxPageNumber));
				throw new HttpResponseException(response);
			}
		}

		public static Guid ParseAndValidateId(this ApiController apiController, HttpRequestMessage request, string id)
		{
			Guid idParsed = ParamParser.ParseGuid(id);
			if (idParsed == Guid.Empty)
			{
				var response = request.CreateErrorResponse(HttpStatusCode.BadRequest, String.Format("Id '{0}' is not a GUID", id));
				throw new HttpResponseException(response);
			}
			return idParsed;
		}

		public static Guid ParseAndValidateProjectId(this ApiController apiController, HttpRequestMessage request, string projectId)
		{
			Guid projectIdParsed = ParamParser.ParseGuid(projectId);
			if (projectIdParsed == Guid.Empty)
			{
				var response = request.CreateErrorResponse(HttpStatusCode.BadRequest, String.Format("Project Id '{0}' is not a GUID", projectId));
				throw new HttpResponseException(response);
			}
			return projectIdParsed;
		}

		public static Guid ParseAndValidateDatabaseId(this ApiController apiController, HttpRequestMessage request, string databaseId)
		{
			Guid databaseIdParsed = ParamParser.ParseGuid(databaseId);
			if (databaseIdParsed == Guid.Empty)
			{
				var response = request.CreateErrorResponse(HttpStatusCode.BadRequest, String.Format("Database Id '{0}' is not a GUID", databaseId));
				throw new HttpResponseException(response);
			}
			return databaseIdParsed;
		}

		public static Guid ParseAndValidateEntityId(this ApiController apiController, HttpRequestMessage request, string entityId)
		{
			Guid entityIdParsed = ParamParser.ParseGuid(entityId);
			if (entityIdParsed == Guid.Empty)
			{
				var response = request.CreateErrorResponse(HttpStatusCode.BadRequest, String.Format("Entity Id '{0}' is not a GUID", entityId));
				throw new HttpResponseException(response);
			}
			return entityIdParsed;
		}

		public static Guid ParseAndValidateRelationId(this ApiController apiController, HttpRequestMessage request, string relationId)
		{
			Guid relationIdParsed = ParamParser.ParseGuid(relationId);
			if (relationIdParsed == Guid.Empty)
			{
				var response = request.CreateErrorResponse(HttpStatusCode.BadRequest, String.Format("Relation Id '{0}' is not a GUID", relationId));
				throw new HttpResponseException(response);
			}
			return relationIdParsed;
		}

		public static Guid ParseAndValidateBaseEnumId(this ApiController apiController, HttpRequestMessage request, string baseEnumId)
		{
			Guid baseEnumIdParsed = ParamParser.ParseGuid(baseEnumId);
			if (baseEnumIdParsed == Guid.Empty)
			{
				var response = request.CreateErrorResponse(HttpStatusCode.BadRequest, String.Format("Base enum Id '{0}' is not a GUID", baseEnumId));
				throw new HttpResponseException(response);
			}
			return baseEnumIdParsed;
		}

		#endregion Validation Methods

		#region Object Retrieval Methods

		public static Project RetrieveProject(this ApiController apiController, HttpRequestMessage request, Guid id)
		{
			var getObject = new GetObject<Project>();
			Project project = getObject.Execute(id);

			if (project == null)
			{
				var response = request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("Project with id '{0}' does not exist", id));
				throw new HttpResponseException(response);
			}

			return project;
		}

		public static Database RetrieveDatabase(this ApiController apiController, HttpRequestMessage request, Guid id, Project project)
		{
			var getDatabase = new GetObject<Database>();
			Database database = getDatabase.Execute(id);

			if (database == null)
			{
				var response = request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("Database with id '{0}' does not exist", id));
				throw new HttpResponseException(response);
			}

			if (database.ProjectId != project.Id)
			{
				var response = request.CreateErrorResponse(
					HttpStatusCode.BadRequest,
					String.Format("Database with id '{0}' does not belong to the project with id '{1}'", database.Id, project.Id));
				throw new HttpResponseException(response);
			}

			return database;
		}

		public static DataType RetrieveDataType(this ApiController apiController, HttpRequestMessage request, Guid id, Database database)
		{
			var getDataType = new GetObject<DataType>();
			DataType dataType = getDataType.Execute(id);

			if (dataType == null)
			{
				var response = request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("Data type with id '{0}' does not exist", id));
				throw new HttpResponseException(response);
			}

			if (dataType.DatabaseId != database.Id)
			{
				var response = request.CreateErrorResponse(
					HttpStatusCode.BadRequest,
					String.Format("Data type with id '{0}' does not belong to the database with id '{1}'", dataType.Id, database.Id));
				throw new HttpResponseException(response);
			}

			return dataType;
		}

		public static Entity RetrieveEntity(this ApiController apiController, HttpRequestMessage request, Guid id, Database database)
		{
			var getEntity = new GetObject<Entity>();
			Entity entity = getEntity.Execute(id);

			if (entity == null)
			{
				var response = request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("Entity with id '{0}' does not exist", id));
				throw new HttpResponseException(response);
			}

			if (entity.DatabaseId != database.Id)
			{
				var response = request.CreateErrorResponse(
					HttpStatusCode.BadRequest,
					String.Format("Entity with id '{0}' does not belong to the database with id '{1}'", entity.Id, database.Id));
				throw new HttpResponseException(response);
			}

			return entity;
		}

		public static RESTModel.Attribute RetrieveAttribute(this ApiController apiController, HttpRequestMessage request, Guid id, Entity entity)
		{
			var getAttribute = new GetObject<RESTModel.Attribute>();
			RESTModel.Attribute attribute = getAttribute.Execute(id);

			if (attribute == null)
			{
				var response = request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("Attribute with id '{0}' does not exist", id));
				throw new HttpResponseException(response);
			}

			if (attribute.EntityId != entity.Id)
			{
				var response = request.CreateErrorResponse(
					HttpStatusCode.BadRequest,
					String.Format("Attribute with id '{0}' does not belong to the entity with id '{1}'", attribute.Id, entity.Id));
				throw new HttpResponseException(response);
			}

			return attribute;
		}

		public static Relation RetrieveRelation(this ApiController apiController, HttpRequestMessage request, Guid id, Entity entity)
		{
			var getRelation = new GetObject<Relation>();
			Relation relation = getRelation.Execute(id);

			if (relation == null)
			{
				var response = request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("Relation with id '{0}' does not exist", id));
				throw new HttpResponseException(response);
			}

			if (relation.ForeignEntityId != entity.Id)
			{
				var response = request.CreateErrorResponse(
					HttpStatusCode.BadRequest,
					String.Format("Relation with id '{0}' does not belong to the entity with id '{1}'", relation.Id, entity.Id));
				throw new HttpResponseException(response);
			}

			return relation;
		}

		public static RelationItem RetrieveRelationItem(this ApiController apiController, HttpRequestMessage request, Guid id, Relation relation)
		{
			var getRelationItem = new GetObject<RelationItem>();
			RelationItem relationItem = getRelationItem.Execute(id);

			if (relationItem == null)
			{
				var response = request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("Relation item with id '{0}' does not exist", id));
				throw new HttpResponseException(response);
			}

			if (relationItem.RelationId != relation.Id)
			{
				var response = request.CreateErrorResponse(
					HttpStatusCode.BadRequest,
					String.Format("Relation item with id '{0}' does not belong to the relation with id '{1}'", relationItem.Id, relation.Id));
				throw new HttpResponseException(response);
			}

			return relationItem;
		}

		public static BaseEnum RetrieveBaseEnum(this ApiController apiController, HttpRequestMessage request, Guid id, Database database)
		{
			var getBaseEnum = new GetObject<BaseEnum>();
			BaseEnum baseEnum = getBaseEnum.Execute(id);

			if (baseEnum == null)
			{
				var response = request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("Base enum with id '{0}' does not exist", id));
				throw new HttpResponseException(response);
			}

			if (baseEnum.DatabaseId != database.Id)
			{
				var response = request.CreateErrorResponse(
					HttpStatusCode.BadRequest,
					String.Format("Base enum with id '{0}' does not belong to the database with id '{1}'", baseEnum.Id, database.Id));
				throw new HttpResponseException(response);
			}

			return baseEnum;
		}

		public static BaseEnumValue RetrieveBaseEnumValue(this ApiController apiController, HttpRequestMessage request, Guid id, BaseEnum baseEnum)
		{
			var getBaseEnumValue = new GetObject<BaseEnumValue>();
			BaseEnumValue baseEnumValue = getBaseEnumValue.Execute(id);

			if (baseEnumValue == null)
			{
				var response = request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("Base enum value with id '{0}' does not exist", id));
				throw new HttpResponseException(response);
			}

			if (baseEnumValue.BaseEnumId != baseEnum.Id)
			{
				var response = request.CreateErrorResponse(
					HttpStatusCode.BadRequest,
					String.Format("Base enum value with id '{0}' does not belong to the base enum with id '{1}'", baseEnumValue.Id, baseEnum.Id));
				throw new HttpResponseException(response);
			}

			return baseEnumValue;
		}

		#endregion Object Retrieval Methods
	}
}