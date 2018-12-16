using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Artemkv.Darwin.Server.Mapping;
using Artemkv.Darwin.Server.RESTHelpers;
using Artemkv.Darwin.Server.RESTModel;

namespace Artemkv.Darwin.Server
{
	public class BookDTO : PersistentObject
	{
		[SimpleProperty]
		public string Title { get; set; }
		[SimpleProperty]
		public int Year { get; set; }

		public Guid AuthorId { get; set; }

		public string FullTitle 
		{
			get
			{
				return Title + ", " + Year.ToString();
			}
		}

		public override Type PersistentType
		{
			get { return typeof(Book); }
		}

		public override void AddLinks(LinkBuilder linkBuilder)
		{
			throw new NotImplementedException();
		}
	}
}
