If Not Exists(Select 1 From sys.tables Where name = 'Solution')
Begin
	Create Table dbo.Solution 
	(
		Id					int	Identity(1,1) NOT NULL,
		Name				varchar(1024) NOT NULL,
		RepositoryId		int NOT NULL,
		CreateUserId		int NOT NULL,
		CreateDateTime		dateTime NOT NULL,
		ModifyUserId		int NOT NULL,
		ModifyDateTime		dateTime NOT NULL,
		CONSTRAINT [PK_Solution] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)
	)	
End 
GO


If Not Exists(Select * From sys.foreign_keys  Where name = 'FK_Solution_Repository')
Begin
	ALTER TABLE [dbo].[Solution]  WITH CHECK ADD  CONSTRAINT [FK_Solution_Repository] FOREIGN KEY([RepositoryId]) REFERENCES [dbo].[Repository] ([Id])
End 
Go