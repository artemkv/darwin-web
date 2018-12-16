INSERT INTO [dbo].[Project] ([Id], [ProjectName])
     VALUES ('09C60021-A9DB-45E9-98E3-EA8E824DC639', 'Demo')
GO

INSERT INTO [dbo].[Database] ([Id], [DBName], [ConnectionString], [ProjectId])
     VALUES ('C014E8A8-FC75-435A-95E4-9CF923D11A9C', 'TVShows', '<Use this connection string to import objects from existing database>', '09C60021-A9DB-45E9-98E3-EA8E824DC639')
GO

INSERT INTO [dbo].[BaseEnum] ([Id], [BaseEnumName], [DatabaseId])
     VALUES ('67A581FB-AD43-4C3F-A500-1388C97C7659', 'FalseTrue', 'C014E8A8-FC75-435A-95E4-9CF923D11A9C')
           , ('9ABB7F2F-2D83-4947-A6F3-691720CF33CD', 'Sex', 'C014E8A8-FC75-435A-95E4-9CF923D11A9C')
           , ('4FB128D5-9199-45DF-8664-486A7446A16B', 'DayOfWeek', 'C014E8A8-FC75-435A-95E4-9CF923D11A9C')
GO

INSERT INTO [dbo].[BaseEnumValue] ([Id], [Name], [Value], [BaseEnumId])
     VALUES ( '2F69124D-71CF-4280-8018-CC9B1440A3FB', 'False', 0, '67A581FB-AD43-4C3F-A500-1388C97C7659')
           , ( 'BF5334B3-E627-495E-A857-0F1CBCFE0301', 'True', 1, '67A581FB-AD43-4C3F-A500-1388C97C7659')
           , ( '602FCA19-1E70-4937-972C-45015B737A0E', 'Male', 0, '9ABB7F2F-2D83-4947-A6F3-691720CF33CD')
           , ( '7B67D1C4-F850-4B37-9240-E4F3345AAFDA', 'Female', 1, '9ABB7F2F-2D83-4947-A6F3-691720CF33CD')
           , ( '9BDF17EE-233F-4309-9EC1-E735B6984E78', 'Monday', 0, '4FB128D5-9199-45DF-8664-486A7446A16B')
           , ( 'E6193557-6F3F-4D93-958F-70B56022EB6A', 'Tuesday', 1, '4FB128D5-9199-45DF-8664-486A7446A16B')
           , ( 'C2230A62-102F-4307-B3FE-B2E68756203F', 'Wednesday', 2, '4FB128D5-9199-45DF-8664-486A7446A16B')
           , ( '240EF14B-D580-4CE1-B2B4-43CA9BD1869E', 'Thursday', 3, '4FB128D5-9199-45DF-8664-486A7446A16B')
           , ( 'BA9561B8-50A3-475B-8626-D09A1AF739FB', 'Friday', 4, '4FB128D5-9199-45DF-8664-486A7446A16B')
           , ( 'D458DEA5-FAF2-44C3-A3E7-0EC9B4F2ADBB', 'Saturday', 5, '4FB128D5-9199-45DF-8664-486A7446A16B')
           , ( 'BE2EAA00-9C24-42C3-B2B4-6F1009760E0A', 'Sunday', 6, '4FB128D5-9199-45DF-8664-486A7446A16B')
GO

INSERT INTO [dbo].[DataType]([Id], [TypeName], [DatabaseId], [HasLength], [BaseEnumId])
     VALUES ('4E979AB8-A633-40B4-92EB-EB062166F758', 'Boolean', 'C014E8A8-FC75-435A-95E4-9CF923D11A9C', 0, '67A581FB-AD43-4C3F-A500-1388C97C7659')
           , ('427AC517-3C47-40AF-B211-F7BD300778DB', 'Int8', 'C014E8A8-FC75-435A-95E4-9CF923D11A9C', 0, NULL)
           , ('7160B1D4-09DE-483E-AADD-687D662AAE21', 'Int16', 'C014E8A8-FC75-435A-95E4-9CF923D11A9C', 0, NULL)
           , ('7E159BB7-9C3F-48DA-B41F-0E1DE48DBB13', 'Int32', 'C014E8A8-FC75-435A-95E4-9CF923D11A9C', 0, NULL)
           , ('A61BDD51-7C43-4E42-9885-E6A02DC12CB7', 'Int64', 'C014E8A8-FC75-435A-95E4-9CF923D11A9C', 0, NULL)
           , ('49DAF0DB-2D5B-43A2-9C91-DE8DA805BD64', 'Real32', 'C014E8A8-FC75-435A-95E4-9CF923D11A9C', 0, NULL)
           , ('77AD6C0D-7EF6-4E6D-8759-FE63EEB12978', 'Real64', 'C014E8A8-FC75-435A-95E4-9CF923D11A9C', 0, NULL)
           , ('0479DC98-FC64-4624-B4B0-330D5E185EE2', 'Char', 'C014E8A8-FC75-435A-95E4-9CF923D11A9C', 1, NULL)
           , ('97FB54C2-10D4-4735-9A45-D4591FBB6211', 'NChar', 'C014E8A8-FC75-435A-95E4-9CF923D11A9C', 1, NULL)
           , ('55207DD0-A028-4C0B-B437-45486060A2BF', 'VarChar', 'C014E8A8-FC75-435A-95E4-9CF923D11A9C', 1, NULL)
           , ('29B1C6BF-EBD1-44EB-9B67-6C1594B0E8DE', 'NVarChar', 'C014E8A8-FC75-435A-95E4-9CF923D11A9C', 1, NULL)
           , ('EA9A7FE5-C9BC-45EB-A205-1EF036B62BDC', 'Guid', 'C014E8A8-FC75-435A-95E4-9CF923D11A9C', 0, NULL)
           , ('B4432D55-47D3-459B-99D6-04629FCEFE16', 'DateTime', 'C014E8A8-FC75-435A-95E4-9CF923D11A9C', 0, NULL)
           , ('527BB363-46BE-4F37-BE66-FCC1FC2378E6', 'Sex', 'C014E8A8-FC75-435A-95E4-9CF923D11A9C', 0, '9ABB7F2F-2D83-4947-A6F3-691720CF33CD')
           , ('39AB1EE9-D765-43EE-9462-48C720CB35AF', 'Description', 'C014E8A8-FC75-435A-95E4-9CF923D11A9C', 0, NULL)
GO

INSERT INTO [dbo].[Entity]([Id], [SchemaName], [EntityName], [DatabaseId])
     VALUES ('2F8AAF45-DBAF-441B-BE7C-11753DAFDC80', 'dbo', 'Show', 'C014E8A8-FC75-435A-95E4-9CF923D11A9C')
           , ('F8033670-279D-4745-B25F-1390B6332947', 'dbo', 'Season', 'C014E8A8-FC75-435A-95E4-9CF923D11A9C')
           , ('B5973996-BF34-444D-95D2-204E92919178', 'dbo', 'Episode', 'C014E8A8-FC75-435A-95E4-9CF923D11A9C')
           , ('0501296F-70AA-4226-B178-6A55671528CF', 'dbo', 'Director', 'C014E8A8-FC75-435A-95E4-9CF923D11A9C')
           , ('3173B8FD-135C-4AF6-950F-6EDF987170FA', 'dbo', 'Person', 'C014E8A8-FC75-435A-95E4-9CF923D11A9C')
           , ('0FC1DF4F-BEB2-4E68-855A-8856F5822D19', 'dbo', 'Star', 'C014E8A8-FC75-435A-95E4-9CF923D11A9C')
           , ('C26B1376-C165-4DF7-9C57-9AD32B0DB459', 'dbo', 'Genre', 'C014E8A8-FC75-435A-95E4-9CF923D11A9C')
           , ('FDB4EAE8-7582-4B2A-9677-CF72B0649D9E', 'dbo', 'EpisodeGenre', 'C014E8A8-FC75-435A-95E4-9CF923D11A9C')
GO

INSERT INTO [dbo].[Attribute] ([Id], [AttributeName], [DataTypeId], [IsRequired], [EntityId], [AttributeLength], [IsPrimaryKey])
     VALUES
			-- Director
           ('B9D9FACB-835E-4C1D-A962-42DBBE09E327',		'ID',			'EA9A7FE5-C9BC-45EB-A205-1EF036B62BDC', 1, '0501296F-70AA-4226-B178-6A55671528CF', 0,	1)
           , ('88329DFB-F2AA-4F4E-A41C-4FE08C0280F0',	'PersonID',		'EA9A7FE5-C9BC-45EB-A205-1EF036B62BDC', 1, '0501296F-70AA-4226-B178-6A55671528CF', 0,	0)
           , ('238B1716-F760-4DC7-9B07-59D22FB4ECD9',	'EpisodeID',	'EA9A7FE5-C9BC-45EB-A205-1EF036B62BDC', 1, '0501296F-70AA-4226-B178-6A55671528CF', 0,	0)
			-- Person
           , ('BB872350-AB7F-4518-B1D6-6B1CFFEB9648',	'ID',			'EA9A7FE5-C9BC-45EB-A205-1EF036B62BDC', 1, '3173B8FD-135C-4AF6-950F-6EDF987170FA', 0,	1)
           , ('F9BA2FB1-1581-4347-94F9-8A199036BE45',	'FirstName',	'29B1C6BF-EBD1-44EB-9B67-6C1594B0E8DE', 1, '3173B8FD-135C-4AF6-950F-6EDF987170FA', 255, 0)
           , ('29942C4B-7ADD-4D22-BEF1-B17216FE8975',	'LastName',		'29B1C6BF-EBD1-44EB-9B67-6C1594B0E8DE', 1, '3173B8FD-135C-4AF6-950F-6EDF987170FA', 255, 0)
           , ('DB7A53CC-7329-4E29-8D67-E8F770C11B5A',	'Sex',			'527BB363-46BE-4F37-BE66-FCC1FC2378E6', 0, '3173B8FD-135C-4AF6-950F-6EDF987170FA', 0,	0)
           , ('C5104CB9-102E-4A97-944C-EFD791A7A2EE',	'DateOfBirth',	'B4432D55-47D3-459B-99D6-04629FCEFE16', 0, '3173B8FD-135C-4AF6-950F-6EDF987170FA', 0,	0)
           -- Genre
           , ('5ABCC6CD-A9EB-475E-98F1-F887372144A1',	'ID',			'EA9A7FE5-C9BC-45EB-A205-1EF036B62BDC', 1, 'C26B1376-C165-4DF7-9C57-9AD32B0DB459', 0,	1)
           , ('C99D7A7D-F251-4D9F-AB8E-FFA8528F8335',	'Title',		'29B1C6BF-EBD1-44EB-9B67-6C1594B0E8DE', 1, 'C26B1376-C165-4DF7-9C57-9AD32B0DB459', 255, 0)
           -- Season
           , ('D14D672A-D886-4981-9870-02FFA1C58E12',	'ID',			'EA9A7FE5-C9BC-45EB-A205-1EF036B62BDC', 1, 'F8033670-279D-4745-B25F-1390B6332947', 0,	1)
           , ('779E9330-31A3-4611-8B5A-85467E24C006',	'SeasonNumber',	'7E159BB7-9C3F-48DA-B41F-0E1DE48DBB13', 1, 'F8033670-279D-4745-B25F-1390B6332947', 0,	0)
           , ('99AC6A27-72EB-462D-9030-C3B9827BE98D',	'ShowID',		'EA9A7FE5-C9BC-45EB-A205-1EF036B62BDC', 1, 'F8033670-279D-4745-B25F-1390B6332947', 0,	0)
           -- Show
           , ('C42D7456-07EF-4438-B8E6-CC574DBC563D',	'ID',			'EA9A7FE5-C9BC-45EB-A205-1EF036B62BDC', 1, '2F8AAF45-DBAF-441B-BE7C-11753DAFDC80', 0,	1)
           , ('3D9055CA-9DF2-4FB7-81D3-D68D03130799',	'Title',		'29B1C6BF-EBD1-44EB-9B67-6C1594B0E8DE', 1, '2F8AAF45-DBAF-441B-BE7C-11753DAFDC80', 255, 0)
           -- EpisodeGenre
           , ('3ABB35C7-D702-41FE-B41E-DB09C9649E6A',	'EpisodeID',	'EA9A7FE5-C9BC-45EB-A205-1EF036B62BDC', 1, 'FDB4EAE8-7582-4B2A-9677-CF72B0649D9E', 0,	1)
           , ('14DEE16A-BAFE-4B10-A644-DF7AF7F31F9F',	'GenreID',		'EA9A7FE5-C9BC-45EB-A205-1EF036B62BDC', 1, 'FDB4EAE8-7582-4B2A-9677-CF72B0649D9E', 0,	1)
           -- Star
           , ('075FAFBE-FBE9-425F-89C3-EF029828F453',	'ID',			'EA9A7FE5-C9BC-45EB-A205-1EF036B62BDC', 1, '0FC1DF4F-BEB2-4E68-855A-8856F5822D19', 0,	1)
           , ('0A85F9EA-600C-4782-A90D-FEADB7C77D8C',	'PersonID',		'EA9A7FE5-C9BC-45EB-A205-1EF036B62BDC', 1, '0FC1DF4F-BEB2-4E68-855A-8856F5822D19', 0,	0)
           , ('E7258E09-C914-4D87-80D7-FFD7AE51076B',	'EpisodeID',	'EA9A7FE5-C9BC-45EB-A205-1EF036B62BDC', 1, '0FC1DF4F-BEB2-4E68-855A-8856F5822D19', 0,	0)
           , ('7779BF41-CD23-4469-996D-06C8AB61791F',	'IsGuest',		'EA9A7FE5-C9BC-45EB-A205-1EF036B62BDC', 1, '0FC1DF4F-BEB2-4E68-855A-8856F5822D19', 0,	0)
			-- Episode
           , ('2280760B-CD07-41A1-B9BD-0FC0D9068651',	'ID',			'EA9A7FE5-C9BC-45EB-A205-1EF036B62BDC', 1, 'B5973996-BF34-444D-95D2-204E92919178', 0,	1)
           , ('DBDD59AA-AADF-495B-ACA5-14FD8F9869DF',	'Title',		'29B1C6BF-EBD1-44EB-9B67-6C1594B0E8DE', 1, 'B5973996-BF34-444D-95D2-204E92919178', 255,	0)
           , ('004DB8F9-D246-4B45-847F-5230C3BB8FE2',	'ReleaseDate',	'B4432D55-47D3-459B-99D6-04629FCEFE16', 1, 'B5973996-BF34-444D-95D2-204E92919178', 0,	0)
           , ('3D488724-EE40-44A8-9EB7-6FB9C8AA5448',	'EpisodeNumber','7E159BB7-9C3F-48DA-B41F-0E1DE48DBB13', 1, 'B5973996-BF34-444D-95D2-204E92919178', 0,	0)
           , ('99C8F3E9-7655-4792-AB2B-7C7F1B6DF056',	'LengthMin',	'7E159BB7-9C3F-48DA-B41F-0E1DE48DBB13', 1, 'B5973996-BF34-444D-95D2-204E92919178', 0,	0)
           , ('55751CBE-8639-4DFA-8445-906CD911AAB3',	'SeasonID',		'EA9A7FE5-C9BC-45EB-A205-1EF036B62BDC', 1, 'B5973996-BF34-444D-95D2-204E92919178', 0,	0)
           , ('C203B619-3B63-44AD-A3CB-B0303FCCE7A3',	'Storyline',	'39AB1EE9-D765-43EE-9462-48C720CB35AF', 0, 'B5973996-BF34-444D-95D2-204E92919178', 0,	0)
           , ('CC414398-B34B-4B2E-8D16-D8284F40BD26',	'IsPilot',		'4E979AB8-A633-40B4-92EB-EB062166F758', 1, 'B5973996-BF34-444D-95D2-204E92919178', 0,	0)
GO

INSERT INTO [dbo].[Relation] ([Id], [RelationName], [ForeignEntityId], [PrimaryEntityId], [OneToOne], [AtLeastOne])
     VALUES ('F61125D9-1689-4A9C-AEBD-05A08CFE8C86', 'Director_Person', '0501296F-70AA-4226-B178-6A55671528CF', '3173B8FD-135C-4AF6-950F-6EDF987170FA', 0, 0)
			, ('F0E8829E-59CA-4103-B0E9-1AB633FAECD2', 'Director_Episode', '0501296F-70AA-4226-B178-6A55671528CF', 'B5973996-BF34-444D-95D2-204E92919178', 0, 0)
			, ('D26A7B93-4A63-4B51-9901-2866D7DDA910', 'Star_Person', '0FC1DF4F-BEB2-4E68-855A-8856F5822D19', '3173B8FD-135C-4AF6-950F-6EDF987170FA', 0, 0)
			, ('31BF390B-DC1B-4908-8C0F-2BA425057A6B', 'Star_Episode', '0FC1DF4F-BEB2-4E68-855A-8856F5822D19', 'B5973996-BF34-444D-95D2-204E92919178', 0, 0)
			
			, ('0C7D0515-47BA-41D4-8BCC-383A453258D3', 'Episode_Season', 'B5973996-BF34-444D-95D2-204E92919178', 'F8033670-279D-4745-B25F-1390B6332947', 0, 0)
			, ('AE677F1D-FAC9-4BF9-A828-4B6B32A1DEC0', 'Season_Show', 'F8033670-279D-4745-B25F-1390B6332947', '2F8AAF45-DBAF-441B-BE7C-11753DAFDC80', 0, 0)
			, ('93260759-4952-43AD-9F69-5643B0D2B54A', 'EpisodeGenre_Episode', 'FDB4EAE8-7582-4B2A-9677-CF72B0649D9E', 'B5973996-BF34-444D-95D2-204E92919178', 0, 0)
			, ('41D74890-1745-47CC-A22E-6D013F19951A', 'EpisodeGenre_Genre', 'FDB4EAE8-7582-4B2A-9677-CF72B0649D9E', 'C26B1376-C165-4DF7-9C57-9AD32B0DB459', 0, 0)
GO

INSERT INTO [dbo].[RelationItem] ([Id], [RelationId], [PrimaryAttributeId], [ForeignAttributeId])
     VALUES ('C19BAF81-39F6-4399-A455-010CB13890BC', 'F61125D9-1689-4A9C-AEBD-05A08CFE8C86', 'BB872350-AB7F-4518-B1D6-6B1CFFEB9648', '88329DFB-F2AA-4F4E-A41C-4FE08C0280F0')
			, ('9711DB85-60BB-49D4-A577-3D24C78F2837', 'F0E8829E-59CA-4103-B0E9-1AB633FAECD2', '2280760B-CD07-41A1-B9BD-0FC0D9068651', '238B1716-F760-4DC7-9B07-59D22FB4ECD9')
			, ('43E44EB1-76A6-4AFF-AE46-3DB7518A8EA2', 'D26A7B93-4A63-4B51-9901-2866D7DDA910', 'BB872350-AB7F-4518-B1D6-6B1CFFEB9648', '0A85F9EA-600C-4782-A90D-FEADB7C77D8C')
			, ('A804A0FE-2744-4772-86E0-41A33D50BF12', '31BF390B-DC1B-4908-8C0F-2BA425057A6B', '2280760B-CD07-41A1-B9BD-0FC0D9068651', 'E7258E09-C914-4D87-80D7-FFD7AE51076B')
			, ('480C0A19-73EE-4598-A937-596E17B5F369', '0C7D0515-47BA-41D4-8BCC-383A453258D3', 'D14D672A-D886-4981-9870-02FFA1C58E12', '55751CBE-8639-4DFA-8445-906CD911AAB3')
			, ('2CB5E878-9973-41B2-AE4A-6238F18F6629', 'AE677F1D-FAC9-4BF9-A828-4B6B32A1DEC0', 'C42D7456-07EF-4438-B8E6-CC574DBC563D', '99AC6A27-72EB-462D-9030-C3B9827BE98D')
			, ('1B9FF790-8826-4AAE-B4D0-6BCA700D89B8', '93260759-4952-43AD-9F69-5643B0D2B54A', '2280760B-CD07-41A1-B9BD-0FC0D9068651', '3ABB35C7-D702-41FE-B41E-DB09C9649E6A')
			, ('A634F929-9B0A-4018-BE65-8DD1AE7D4037', '41D74890-1745-47CC-A22E-6D013F19951A', '5ABCC6CD-A9EB-475E-98F1-F887372144A1', '14DEE16A-BAFE-4B10-A644-DF7AF7F31F9F')
GO

INSERT INTO [dbo].[Diagram] ([Id], [DiagramName], [DatabaseId], [PaperSize], [IsVertical], [IsAdjusted], [ShowModality])
     VALUES ('0AB0C76C-2030-4573-ACD4-5C4914A163FD', 'Main', 'C014E8A8-FC75-435A-95E4-9CF923D11A9C', 4, 0, 1, 1)
GO

INSERT INTO [dbo].[DiagramEntity] ([Id], [DiagramId], [EntityId], [X], [Y])
     VALUES ('7AFADFA7-4C93-4813-A9E2-1197CE00E0E2', '0AB0C76C-2030-4573-ACD4-5C4914A163FD', '0501296F-70AA-4226-B178-6A55671528CF', 684, 252)
			, ('8BC90310-6C22-4AF5-BFA6-471062F21DC0', '0AB0C76C-2030-4573-ACD4-5C4914A163FD', 'B5973996-BF34-444D-95D2-204E92919178', 400, 324)
			, ('75EF5C22-7031-4B39-A0F6-4E5982DAF090', '0AB0C76C-2030-4573-ACD4-5C4914A163FD', 'FDB4EAE8-7582-4B2A-9677-CF72B0649D9E', 132, 396)
			, ('9C699691-5334-4DB5-9682-5108BCC5E60B', '0AB0C76C-2030-4573-ACD4-5C4914A163FD', 'C26B1376-C165-4DF7-9C57-9AD32B0DB459', 132, 604)
			, ('A7D219DC-F535-472B-8C3D-6B437D77E6E8', '0AB0C76C-2030-4573-ACD4-5C4914A163FD', '3173B8FD-135C-4AF6-950F-6EDF987170FA', 936, 356)
			, ('61AE810B-ADA9-49AE-9F9B-76706E9A9946', '0AB0C76C-2030-4573-ACD4-5C4914A163FD', 'F8033670-279D-4745-B25F-1390B6332947', 400, 84)
			, ('BCCFEA40-3EC2-49EB-A706-811085C4CDC8', '0AB0C76C-2030-4573-ACD4-5C4914A163FD', '2F8AAF45-DBAF-441B-BE7C-11753DAFDC80', 116, 96)
			, ('BD7C0851-CA09-4884-9E1C-904A066CCDFC', '0AB0C76C-2030-4573-ACD4-5C4914A163FD', '0FC1DF4F-BEB2-4E68-855A-8856F5822D19', 680, 492)
GO