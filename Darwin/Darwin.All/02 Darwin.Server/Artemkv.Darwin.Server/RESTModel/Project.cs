using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Artemkv.Darwin.Server.Mapping;
using System.Runtime.Serialization;

using ERModel = Artemkv.Darwin.ERModel;
using Artemkv.Darwin.Server.RESTHelpers;

namespace Artemkv.Darwin.Server.RESTModel
{
	[DataContract(Name = "project", Namespace = "")]
	public class Project : PersistentObject
	{
		public Project()
		{
		}

		[DataMember(Name = "projectName")]
		[SimpleProperty]
		public string ProjectName { get; set; }

		[IgnoreDataMember]
		public override Type PersistentType
		{
			get { return typeof(ERModel.Project); }
		}

		public override void AddLinks(LinkBuilder linkBuilder)
		{
			linkBuilder.ProjectId = this.Id;

			this.Links.Add(new Link()
			{
				Href = linkBuilder.ProjectSelf(this.Id),
				Rel = LinkRelations.Self
			});

			this.Links.Add(new Link()
			{
				Href = linkBuilder.ProjectNodes(this.Id.ToString(), TreeNodePath.Root),
				Rel = LinkRelations.GetNodes
			});
			this.Links.Add(new Link()
			{
				Href = linkBuilder.ProjectDatabases(),
				Rel = LinkRelations.Databases
			});
		}

		public override string ToString()
		{
			return ProjectName;
		}
	}
}
