using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RESTModel = Artemkv.Darwin.Server.RESTModel;
using ERModel = Artemkv.Darwin.ERModel;

namespace Artemkv.Darwin.Server.TransactionScripts
{
	public class GetObject<T> where T : RESTModel.PersistentObject
	{
		public T Execute(Guid objectId)
		{
			T obj = null;

			var serviceLocator = ServiceLocator.GetActive();
			var dataProvider = serviceLocator.DataProvider;

			using (var session = dataProvider.OpenSession())
			{
				using (var transaction = session.BeginTransaction())
				{
					if (typeof(T) == typeof(RESTModel.Project))
					{
						obj = new Assembler().GetDTO(dataProvider.GetObject<ERModel.Project>(objectId, session)) as T;
					}
					else if (typeof(T) == typeof(RESTModel.Database))
					{
						obj = new Assembler().GetDTO(dataProvider.GetObject<ERModel.Database>(objectId, session)) as T;
					}
					else if (typeof(T) == typeof(RESTModel.DataType))
					{
						obj = new Assembler().GetDTO(dataProvider.GetObject<ERModel.DataType>(objectId, session)) as T;
					}
					else if (typeof(T) == typeof(RESTModel.BaseEnum))
					{
						obj = new Assembler().GetDTO(dataProvider.GetObject<ERModel.BaseEnum>(objectId, session)) as T;
					}
					else if (typeof(T) == typeof(RESTModel.BaseEnumValue))
					{
						obj = new Assembler().GetDTO(dataProvider.GetObject<ERModel.BaseEnumValue>(objectId, session)) as T;
					}
					else if (typeof(T) == typeof(RESTModel.Entity))
					{
						obj = new Assembler().GetDTO(dataProvider.GetObject<ERModel.Entity>(objectId, session)) as T;
					}
					else if (typeof(T) == typeof(RESTModel.Attribute))
					{
						obj = new Assembler().GetDTO(dataProvider.GetObject<ERModel.Attribute>(objectId, session)) as T;
					}
					else if (typeof(T) == typeof(RESTModel.Relation))
					{
						obj = new Assembler().GetDTO(dataProvider.GetObject<ERModel.Relation>(objectId, session)) as T;
					}
					else if (typeof(T) == typeof(RESTModel.RelationItem))
					{
						obj = new Assembler().GetDTO(dataProvider.GetObject<ERModel.RelationItem>(objectId, session)) as T;
					}
				}
			}
			return obj;
		}
	}
}
