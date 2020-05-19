If Not Exists(Select 1 From sys.tables Where name = 'RepositoryOwnerApiKey')
Begin
	Create Table Reliance.RepositoryOwnerApiKey 
	(
		Id					bigint	Identity(1,1) NOT NULL,
		CreateUserId		bigint NOT NULL,
		CreateDateTime		dateTime NOT NULL,
		ModifyUserId		bigint NOT NULL,
		ModifyDateTime		dateTime NOT NULL,
		OwnerId				bigint NOT NULL,
		ApiKey				varchar(128) NOT NULL,
		ExpireOn			datetime NULL,
		IsRevoked			bit NOT NULL Default 0,
		Comment				varchar(512) NULL,
		CONSTRAINT [PK_RepositoryOwnerApiKey] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)
	)
End 
GO

If Not Exists(Select * From sys.foreign_keys  Where name = 'FK_Repository_Owner_ApiKey')
Begin
	ALTER TABLE [Reliance].[RepositoryOwnerApiKey]  
		WITH CHECK 
		ADD  CONSTRAINT [FK_Repository_Owner_ApiKey] 
		FOREIGN KEY([OwnerId]) REFERENCES [Reliance].[RepositoryOwner] ([Id])
End 
Go