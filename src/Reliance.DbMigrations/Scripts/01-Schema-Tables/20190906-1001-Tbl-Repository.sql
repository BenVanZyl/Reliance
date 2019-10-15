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