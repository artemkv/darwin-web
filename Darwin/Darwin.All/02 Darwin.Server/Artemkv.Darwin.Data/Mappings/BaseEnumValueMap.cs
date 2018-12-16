using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Artemkv.Darwin.ERModel;
using FluentNHibernate.Mapping;

namespace Artemkv.Darwin.Data.Mappings
{
	public class BaseEnumValueMap : ClassMap<BaseEnumValue>
	{
		public BaseEnumValueMap()
		{
			Id(x => x.Id).GeneratedBy.Assigned();
			Version(x => x.Ts).Generated.Always();

			Map(x => x.Name);
			Map(x => x.Value);

			References(x => x.BaseEnum).Column("BaseEnumId"); // TODO: implement a convention to avoid specifying the foreign key name.
		}
	}
}
