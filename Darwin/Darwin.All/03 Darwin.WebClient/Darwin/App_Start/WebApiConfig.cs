using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Artemkv.Darwin.Common;

namespace Darwin
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.Routes.MapHttpRoute(name: "projects", routeTemplate: "projects", defaults: new { controller = "projects" });
			config.Routes.MapHttpRoute(name: "project", routeTemplate: "project/{id}", defaults: new { controller = "project" });
			config.Routes.MapHttpRoute(name: "getprojectnodes", routeTemplate: "getprojectnodes/{parentids}/{path}", defaults: new { controller = "getprojectnodes" });
			config.Routes.MapHttpRoute(name: "databases", routeTemplate: "project/{projectid}/databases", defaults: new { controller = "databases" });
			config.Routes.MapHttpRoute(name: "database", routeTemplate: "project/{projectid}/database/{id}", defaults: new { controller = "database" });
			config.Routes.MapHttpRoute(name: "datatypes", routeTemplate: "project/{projectid}/database/{databaseid}/datatypes", defaults: new { controller = "datatypes" });
			config.Routes.MapHttpRoute(name: "datatype", routeTemplate: "project/{projectid}/database/{databaseid}/datatype/{id}", defaults: new { controller = "datatype" });
			config.Routes.MapHttpRoute(name: "entities", routeTemplate: "project/{projectid}/database/{databaseid}/entities", defaults: new { controller = "entities" });
			config.Routes.MapHttpRoute(name: "entity", routeTemplate: "project/{projectid}/database/{databaseid}/entity/{id}", defaults: new { controller = "entity" });
			config.Routes.MapHttpRoute(name: "attributes", routeTemplate: "project/{projectid}/database/{databaseid}/entity/{entityId}/attributes", defaults: new { controller = "attributes" });
			config.Routes.MapHttpRoute(name: "attribute", routeTemplate: "project/{projectid}/database/{databaseid}/entity/{entityId}/attribute/{id}", defaults: new { controller = "attribute" });
			config.Routes.MapHttpRoute(name: "relations", routeTemplate: "project/{projectid}/database/{databaseid}/entity/{entityId}/relations", defaults: new { controller = "relations" });
			config.Routes.MapHttpRoute(name: "relation", routeTemplate: "project/{projectid}/database/{databaseid}/entity/{entityId}/relation/{id}", defaults: new { controller = "relation" });
			config.Routes.MapHttpRoute(name: "relationitems", routeTemplate: "project/{projectid}/database/{databaseid}/entity/{entityId}/relation/{relationid}/items", defaults: new { controller = "relationitems" });
			config.Routes.MapHttpRoute(name: "relationitem", routeTemplate: "project/{projectid}/database/{databaseid}/entity/{entityId}/relation/{relationid}/item/{id}", defaults: new { controller = "relationitem" });
			config.Routes.MapHttpRoute(name: "baseenums", routeTemplate: "project/{projectid}/database/{databaseid}/baseenums", defaults: new { controller = "baseenums" });
			config.Routes.MapHttpRoute(name: "baseenum", routeTemplate: "project/{projectid}/database/{databaseid}/baseenum/{id}", defaults: new { controller = "baseenum" });
			config.Routes.MapHttpRoute(name: "baseenumvalues", routeTemplate: "project/{projectid}/database/{databaseid}/baseenum/{baseenumid}/values", defaults: new { controller = "baseenumvalues" });
			config.Routes.MapHttpRoute(name: "baseenumvalue", routeTemplate: "project/{projectid}/database/{databaseid}/baseenum/{baseenumid}/value/{id}", defaults: new { controller = "baseenumvalue" });
			config.Routes.MapHttpRoute(name: "diagrams", routeTemplate: "project/{projectid}/database/{databaseid}/diagrams", defaults: new { controller = "diagrams" });

			config.Routes.MapHttpRoute(name: "testerrors", routeTemplate: "testerrors/{test}", defaults: new { controller = "testerrors" });

			// Enable application-specific media types
			config.Formatters.Add(new DarwinXmlMediaTypeFormatter());
			config.Formatters.Add(new DarwinJsonMediaTypeFormatter());

			config.Filters.Add(new ProfileApiAttribute());
			config.Filters.Add(new HandleApiExceptionAttribute());
		}
	}
}
