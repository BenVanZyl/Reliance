If Not Exists(Select 1 From sys.tables Where name = 'RepositoryOwner')
Begin
	Create Table Reliance.RepositoryOwner 
	(
		Id					bigint	Identity(1,1) NOT NULL,
		CreateUserId		bigint NOT NULL,
		CreateDateTime		dateTime NOT NULL,
		ModifyUserId		bigint NOT NULL,
		ModifyDateTime		dateTime NOT NULL,
		UserId				varchar(450) NOT NULL,
		Name				varchar(128) NOT NULL,
		IsOrganisation		bit NOT NULL Default 0,
		Description			varchar(2048) NULL,
		CONSTRAINT [PK_RepositoryOwner] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)
	)
End 
GO

/*
if ((Select Count(*) From dbo.RepositoryOwner) = 0)
Begin
	Insert Into dbo.RepositoryOwner 
		Values 
		('7d840258-053c-4a16-8b82-f8c9ac1e610f', 'Default', 0, 'Some description', 1, GetDate(), 1, GetDate())
	
End
*/