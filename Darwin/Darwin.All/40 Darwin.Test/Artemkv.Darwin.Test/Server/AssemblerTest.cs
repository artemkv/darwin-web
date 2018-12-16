using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Reflection;

using RESTModel = Artemkv.Darwin.Server.RESTModel;
using ERModel = Artemkv.Darwin.ERModel;

namespace Artemkv.Darwin.Server
{
	[TestFixture]
	public class AssemblerTest
	{
		[Test]
		public void GetDTO_SimpleProp_Test()
		{
			var entity = new ERModel.Entity()
			{
				Id = new Guid("A81DD7C9-3B6B-41D7-95E9-72ECEE645F05"),
				SchemaName = "dbo",
				EntityName = "Author"
			};

			var asm = new Assembler();
			RESTModel.Entity dto = asm.GetDTO(entity) as RESTModel.Entity;

			Assert.AreEqual(entity.Id, dto.Id);
			Assert.AreEqual(entity.SchemaName, dto.SchemaName);
			Assert.AreEqual(entity.EntityName, dto.EntityName);
		}

		[Test]
		public void GetDTO_ObjectProp_Test()
		{
			var dataType = new ERModel.DataType()
			{
				Id = new Guid("3FD6B018-C30A-470A-A4B5-0470366ACAB6"),
				TypeName = "INTEGER"
			};

			var attr = new ERModel.Attribute()
			{
				Id = new Guid("1814BE78-4065-4F64-BD32-1A3FE588D3EA"),
				AttributeName = "FirstName",
				IsRequired = true,

				DataType = dataType // !Important
			};

			var asm = new Assembler();
			RESTModel.Attribute dto = asm.GetDTO(attr) as RESTModel.Attribute;

			Assert.AreEqual(attr.Id, dto.Id);
			Assert.AreEqual(attr.AttributeName, dto.AttributeName);
			Assert.AreEqual(attr.IsRequired, dto.IsRequired);

			Assert.AreEqual(attr.DataType.Id, dto.DataTypeId);
		}

		[Test]
		public void GetDTO_ObjectCollection_Test()
		{
			var dataType = new ERModel.DataType()
			{
				Id = new Guid("3FD6B018-C30A-470A-A4B5-0470366ACAB6"),
				TypeName = "INTEGER"
			};

			var attr1 = new ERModel.Attribute()
			{
				Id = new Guid("1814BE78-4065-4F64-BD32-1A3FE588D3EA"),
				AttributeName = "FirstName",
				IsRequired = true,

				DataType = dataType
			};

			var attr2 = new ERModel.Attribute()
			{
				Id = new Guid("1814BE78-4065-4F64-BD32-1A3FE588D3EA"),
				AttributeName = "LastName",
				IsRequired = true,

				DataType = dataType
			};

			var entity = new ERModel.Entity()
			{
				Id = new Guid("A81DD7C9-3B6B-41D7-95E9-72ECEE645F05"),
				SchemaName = "dbo",
				EntityName = "Author"
			};

			entity.Attributes.Add(attr1);
			entity.Attributes.Add(attr2);

			var asm = new Assembler();
			RESTModel.Entity dto = asm.GetDTO(entity) as RESTModel.Entity;

			Assert.AreEqual(entity.Id, dto.Id);

			Assert.AreEqual(entity.Attributes.Count, dto.Attributes.Count);

			Assert.AreEqual(entity.Attributes[0].Id, dto.Attributes[0].Id);
			Assert.AreEqual(entity.Attributes[0].AttributeName, dto.Attributes[0].AttributeName);
			Assert.AreEqual(entity.Attributes[0].IsRequired, dto.Attributes[0].IsRequired);

			Assert.AreEqual(entity.Attributes[1].Id, dto.Attributes[1].Id);
			Assert.AreEqual(entity.Attributes[1].AttributeName, dto.Attributes[1].AttributeName);
		}

		[Test]
		public void GetDTO_ParentProp_Test()
		{
			var entity = new ERModel.Entity()
			{
				Id = new Guid("A81DD7C9-3B6B-41D7-95E9-72ECEE645F05"),
				SchemaName = "dbo",
				EntityName = "Author"
			};

			var dataType = new ERModel.DataType()
			{
				Id = new Guid("3FD6B018-C30A-470A-A4B5-0470366ACAB6"),
				TypeName = "INTEGER"
			};

			var attr = new ERModel.Attribute()
			{
				Id = new Guid("1814BE78-4065-4F64-BD32-1A3FE588D3EA"),
				AttributeName = "FirstName",
				IsRequired = true,
				DataType = dataType,

				Entity = entity // !important
			};

			var asm = new Assembler();
			RESTModel.Attribute dto = asm.GetDTO(attr) as RESTModel.Attribute;

			Assert.AreEqual(attr.Id, dto.Id);
			Assert.AreEqual(attr.AttributeName, dto.AttributeName);
			Assert.AreEqual(attr.IsRequired, dto.IsRequired);

			Assert.AreEqual(attr.Entity.Id, dto.EntityId);
		}

		[Test]
		public void GetRealObject_Author_Test()
		{
			var dto = new AuthorDTO()
			{
				Id = new Guid("44371CFE-E649-4784-ABE5-4C841D572416"),
				FirstName = "Albert",
				LastName = "Camus"
			};

			var asm = new Assembler();
			Author real = asm.GetRealObject(dto, new MockSession_Author()) as Author;

			Assert.AreEqual(dto.Id, real.Id);
			Assert.AreEqual(dto.FirstName, real.FirstName);
			Assert.AreEqual(dto.LastName, real.LastName);
		}

		[Test]
		public void GetRealObject_AuthorBook_Insert_Test()
		{
			var dto = new AuthorDTO()
			{
				Id = new Guid("44371CFE-E649-4784-ABE5-4C841D572416"),
				FirstName = "Albert",
				LastName = "Camus"
			};

			var book = new BookDTO()
			{
				Id = new Guid("F917EEEA-ADFC-4451-973E-0FF8CE59BECD"),
				Title = "L'Entranger",
				Year = 1942
			};

			dto.Books.Add(book);

			var asm = new Assembler();
			Author real = asm.GetRealObject(dto, new MockSession_Author()) as Author;

			Assert.AreEqual(real.Books.Count, 1);

			Assert.AreEqual(book.Id, real.Books[0].Id);
			Assert.AreEqual(book.Title, real.Books[0].Title);
			Assert.AreEqual(book.Year, real.Books[0].Year);
			Assert.AreEqual(dto.Id, real.Books[0].Author.Id);
		}

		[Test]
		public void GetRealObject_AuthorBook_Update_Test()
		{
			var dto = new AuthorDTO()
			{
				Id = new Guid("44371CFE-E649-4784-ABE5-4C841D572416"),
				FirstName = "Albert",
				LastName = "Camus"
			};

			var book = new BookDTO()
			{
				Id = new Guid("F917EEEA-ADFC-4451-973E-0FF8CE59BECD"),
				Title = "L'Entranger",
				Year = 1942,
				AuthorId = new Guid("44371CFE-E649-4784-ABE5-4C841D572416")
			};

			dto.Books.Add(book);

			var asm = new Assembler();
			Author real = asm.GetRealObject(dto, new MockSession_AuthorWithBook()) as Author;

			Assert.AreEqual(real.Books.Count, 1);

			Assert.AreEqual(book.Id, real.Books[0].Id);
			Assert.AreEqual(book.Title, real.Books[0].Title);
			Assert.AreEqual(book.Year, real.Books[0].Year);
			Assert.AreEqual(dto.Id, real.Books[0].Author.Id);
		}

		[Test]
		public void GetRealObject_AuthorBook_UpdateAndAttach_Test()
		{
			var dto = new AuthorDTO()
			{
				Id = new Guid("44371CFE-E649-4784-ABE5-4C841D572416"),
				FirstName = "Albert",
				LastName = "Camus"
			};

			var book = new BookDTO()
			{
				Id = new Guid("F917EEEA-ADFC-4451-973E-0FF8CE59BECD"),
				Title = "L'Entranger",
				Year = 1942
			};

			dto.Books.Add(book);

			var asm = new Assembler();
			Author real = asm.GetRealObject(dto, new MockSession_AuthorAndBook()) as Author;

			Assert.AreEqual(real.Books.Count, 1);

			Assert.AreEqual(book.Id, real.Books[0].Id);
			Assert.AreEqual(book.Title, real.Books[0].Title);
			Assert.AreEqual(book.Year, real.Books[0].Year);
			Assert.AreEqual(dto.Id, real.Books[0].Author.Id);
		}

		[Test]
		public void GetRealObject_AuthorBook_Delete_Test()
		{
			var dto = new AuthorDTO()
			{
				Id = new Guid("44371CFE-E649-4784-ABE5-4C841D572416"),
				FirstName = "Albert",
				LastName = "Camus"
			};

			var asm = new Assembler();
			Author real = asm.GetRealObject(dto, new MockSession_AuthorWithBook()) as Author;

			Assert.AreEqual(0, real.Books.Count);
		}
	}
}
