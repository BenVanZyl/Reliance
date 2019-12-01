If Not Exists(Select 1 From sys.tables Where name = 'Repository')
Begin
	Create Table dbo.Repository 
	(
		Id					int	Identity(1,1) NOT NULL,
		Name				varchar(1024) NOT NULL,
		CreateUserId		int NOT NULL,
		CreateDateTime		dateTime NOT NULL,
		ModifyUserId		int NOT NULL,
		ModifyDateTime		dateTime NOT NULL,
		CONSTRAINT [PK_Repository] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)
	)
End 
GO

If Not Exists(Select * From sys.columns c inner join sys.tables t on c.object_id = t.object_id 
				Where t.name = 'Repository' and c.name = 'OwnerId')
Begin
	ALTER TABLE [dbo].[Repository]  Add OwnerId int NULL Default NULL
End 
Go

If Not Exists(Select * From sys.foreign_keys  Where name = 'FK_Repository_Owner')
Begin
	ALTER TABLE [dbo].[Repository]  
		WITH CHECK 
		ADD  CONSTRAINT [FK_Repository_Owner] 
		FOREIGN KEY([OwnerId]) REFERENCES [dbo].[RepositoryOwner] ([Id])
End 
Go