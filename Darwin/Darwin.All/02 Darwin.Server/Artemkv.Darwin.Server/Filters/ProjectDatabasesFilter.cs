using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artemkv.Darwin.Server.Filters
{
	public class ProjectDatabasesFilter : ListFilter
	{
		public ProjectDatabasesFilter(Guid projectId)
		{
			if (projectId != Guid.Empty)
			{
				this.ProjectId = projectId;
				this.Add(new ListFilterParameter("Project.Id", ProjectId));
			}
		}

		public Guid ProjectId { get; private set; }
	}
}
