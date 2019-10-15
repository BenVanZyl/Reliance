If Not Exists(Select 1 From sys.tables Where name = 'ProjectPackage')
Begin
	Create Table dbo.ProjectPackage 
	(
		Id					int	Identity(1,1) NOT NULL,
		ProjectId			int NOT NULL,
		PackageId			int NOT NULL,
		CreateUserId		int NOT NULL,
		CreateDateTime		dateTime NOT NULL,
		ModifyUserId		int NOT NULL,
		ModifyDateTime		dateTime NOT NULL,
		CONSTRAINT [PK_ProjectPackage] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)
	)	
End 
GO

If Not Exists(Select * From sys.foreign_keys  Where name = 'FK_ProjectPackage_Project')
Begin
	ALTER TABLE [dbo].[ProjectPackage]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPackage_Project] FOREIGN KEY([ProjectId]) REFERENCES [dbo].[Project] ([Id])
End 
Go

If Not Exists(Select * From sys.foreign_keys  Where name = 'FK_ProjectPackage_Package')
Begin
	ALTER TABLE [dbo].[ProjectPackage]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPackage_Package] FOREIGN KEY([PackageId]) REFERENCES [dbo].[Package] ([Id])
End 
Go

If Not Exists(Select * From sys.indexes Where name = 'ProjectPackageIndex')
Begin
	CREATE UNIQUE NONCLUSTERED INDEX [ProjectPackageIndex] ON [dbo].[ProjectPackage]
	(
		[ProjectId] ASC,
		[PackageId] ASC
	)
End
Go