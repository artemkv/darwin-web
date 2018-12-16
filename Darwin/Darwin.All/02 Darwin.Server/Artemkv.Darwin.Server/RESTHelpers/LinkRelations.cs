using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artemkv.Darwin.Server.RESTHelpers
{
	public static class LinkRelations
	{
		// TODO: This should be configurable! But static, should not depend on the request, otherwise clients cannot rely on relations
		public static readonly string DomainName = "www.darwin.com";

		public static string Self
		{
			get
			{
				return "self";
			}
		}

		public static string Databases
		{
			get
			{
				return String.Format("http://{0}/rels/databases", DomainName);
			}
		}

		public static string DataTypes
		{
			get
			{
				return String.Format("http://{0}/rels/datatypes", DomainName);
			}
		}

		public static string BaseEnums
		{
			get
			{
				return String.Format("http://{0}/rels/baseenums", DomainName);
			}
		}

		public static string Entities
		{
			get
			{
				return String.Format("http://{0}/rels/entities", DomainName);
			}
		}

		public static string Attributes
		{
			get
			{
				return String.Format("http://{0}/rels/attributes", DomainName);
			}
		}

		public static string Relations
		{
			get
			{
				return String.Format("http://{0}/rels/relations", DomainName);
			}
		}

		public static string RelationItems
		{
			get
			{
				return String.Format("http://{0}/rels/relationitems", DomainName);
			}
		}

		public static string Diagrams
		{
			get
			{
				return String.Format("http://{0}/rels/diagrams", DomainName);
			}
		}

		public static string BaseEnumValues
		{
			get
			{
				return String.Format("http://{0}/rels/baseenumvalues", DomainName);
			}
		}

		public static string BaseEnum
		{
			get
			{
				return String.Format("http://{0}/rels/baseenum", DomainName);
			}
		}

		public static string DataType
		{
			get
			{
				return String.Format("http://{0}/rels/datatype", DomainName);
			}
		}

		public static string PrimaryEntity
		{
			get
			{
				return String.Format("http://{0}/rels/primaryentity", DomainName);
			}
		}

		public static string PrimaryAttribute
		{
			get
			{
				return String.Format("http://{0}/rels/primaryattribute", DomainName);
			}
		}

		public static string GetNodes
		{
			get
			{
				return String.Format("http://{0}/rels/getprojectnodes", DomainName);
			}
		}
	}
}
