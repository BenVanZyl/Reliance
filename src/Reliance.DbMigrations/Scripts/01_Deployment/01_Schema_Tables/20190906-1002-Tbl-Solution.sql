If Not Exists(Select 1 From sys.tables Where name = 'Solution')
Begin
	Create Table Reliance.Solution 
	(
		Id					bigint	Identity(1,1) NOT NULL,
		RepositoryId		bigint NOT NULL,
		CreateUserId		bigint NOT NULL,
		CreateDateTime		dateTime NOT NULL,
		ModifyUserId		bigint NOT NULL,
		ModifyDateTime		dateTime NOT NULL,
		Name				varchar(1024) NOT NULL,
		CONSTRAINT [PK_Solution] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)
	)	
End 
GO


If Not Exists(Select * From sys.foreign_keys  Where name = 'FK_Solution_Repository')
Begin
	ALTER TABLE [Reliance].[Solution]  WITH CHECK ADD  CONSTRAINT [FK_Solution_Repository] FOREIGN KEY([RepositoryId]) REFERENCES [Reliance].[Repository] ([Id])
End 
Go