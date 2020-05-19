If Not Exists(Select 1 From sys.tables Where name = 'Project')
Begin
	Create Table Reliance.Project 
	(
		Id					bigint	Identity(1,1) NOT NULL,
		CreateUserId		bigint NOT NULL,
		CreateDateTime		dateTime NOT NULL,
		ModifyUserId		bigint NOT NULL,
		ModifyDateTime		dateTime NOT NULL,
		Name				varchar(255) NOT NULL,
		SolutionId			bigint NOT NULL,
		CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)
	)	
End 
GO

If Not Exists(Select * From sys.foreign_keys  Where name = 'FK_Project_Solution')
Begin
	ALTER TABLE [Reliance].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_Solution] FOREIGN KEY([SolutionId]) REFERENCES [Reliance].[Solution] ([Id])
End 
Go
