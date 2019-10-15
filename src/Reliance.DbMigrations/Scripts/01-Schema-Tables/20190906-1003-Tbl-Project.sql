If Not Exists(Select 1 From sys.tables Where name = 'Project')
Begin
	Create Table dbo.Project 
	(
		Id					int	Identity(1,1) NOT NULL,
		Name				varchar(255) NOT NULL,
		SolutionId			int NOT NULL,
		CreateUserId		int NOT NULL,
		CreateDateTime		dateTime NOT NULL,
		ModifyUserId		int NOT NULL,
		ModifyDateTime		dateTime NOT NULL,
		CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)
	)	
End 
GO

If Not Exists(Select * From sys.foreign_keys  Where name = 'FK_Project_Solution')
Begin
	ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_Solution] FOREIGN KEY([SolutionId]) REFERENCES [dbo].[Solution] ([Id])
End 
Go
