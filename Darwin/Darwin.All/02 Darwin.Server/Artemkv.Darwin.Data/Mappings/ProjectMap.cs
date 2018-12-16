using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Artemkv.Darwin.ERModel;

namespace Artemkv.Darwin.Data.Mappings
{
	public class ProjectMap : ClassMap<Project>
	{
		public ProjectMap()
		{
			Id(x => x.Id).GeneratedBy.Assigned();
			Version(x => x.Ts).Generated.Always();

			Map(x => x.ProjectName);
		}
	}
}
