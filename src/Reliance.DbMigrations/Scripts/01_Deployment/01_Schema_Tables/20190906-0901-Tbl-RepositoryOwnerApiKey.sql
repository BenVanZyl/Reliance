If Not Exists(Select 1 From sys.tables Where name = 'RepositoryOwnerApiKey')
Begin
	Create Table dbo.RepositoryOwnerApiKey 
	(
		Id					int	Identity(1,1) NOT NULL,
		OwnerId				int NOT NULL,
		ApiKey				varchar(128) NOT NULL,
		ExpireOn			datetime NULL,
		IsRevoked			bit NOT NULL Default 0,
		Comment				varchar(512) NULL,
		CreateUserId		int NOT NULL,
		CreateDateTime		dateTime NOT NULL,
		ModifyUserId		int NOT NULL,
		ModifyDateTime		dateTime NOT NULL,
		CONSTRAINT [PK_RepositoryOwnerApiKey] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)
	)
End 
GO

If Not Exists(Select * From sys.foreign_keys  Where name = 'FK_Repository_Owner_ApiKey')
Begin
	ALTER TABLE [dbo].[RepositoryOwnerApiKey]  
		WITH CHECK 
		ADD  CONSTRAINT [FK_Repository_Owner_ApiKey] 
		FOREIGN KEY([OwnerId]) REFERENCES [dbo].[RepositoryOwner] ([Id])
End 
Go