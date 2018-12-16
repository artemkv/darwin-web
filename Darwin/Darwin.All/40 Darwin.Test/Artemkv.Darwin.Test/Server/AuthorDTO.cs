using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Artemkv.Darwin.Server.Mapping;
using Artemkv.Darwin.Server.RESTModel;
using Artemkv.Darwin.Server.RESTHelpers;

namespace Artemkv.Darwin.Server
{
	public class AuthorDTO : PersistentObject
	{
		private List<BookDTO> _books = new List<BookDTO>();

		[SimpleProperty]
		public string FirstName { get; set; }
		[SimpleProperty]
		public string LastName { get; set; }

		[ObjectCollection(parentProperty: "Author")]
		public List<BookDTO> Books
		{
			get
			{
				return _books;
			}
		}

		public override Type PersistentType
		{
			get { return typeof(Author); }
		}

		public override void AddLinks(LinkBuilder linkBuilder)
		{
			throw new NotImplementedException();
		}
	}
}