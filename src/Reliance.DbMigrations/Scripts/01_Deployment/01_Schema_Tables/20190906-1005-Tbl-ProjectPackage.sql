If Not Exists(Select 1 From sys.tables Where name = 'ProjectPackage')
Begin
	Create Table Reliance.ProjectPackage 
	(
		Id					bigint	Identity(1,1) NOT NULL,
		CreateUserId		bigint NOT NULL,
		CreateDateTime		dateTime NOT NULL,
		ModifyUserId		bigint NOT NULL,
		ModifyDateTime		dateTime NOT NULL,
		ProjectId			bigint NOT NULL,
		PackageId			bigint NOT NULL,
		CONSTRAINT [PK_ProjectPackage] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)
	)	
End 
GO

If Not Exists(Select * From sys.foreign_keys  Where name = 'FK_ProjectPackage_Project')
Begin
	ALTER TABLE [Reliance].[ProjectPackage]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPackage_Project] FOREIGN KEY([ProjectId]) REFERENCES [Reliance].[Project] ([Id])
End 
Go

If Not Exists(Select * From sys.foreign_keys  Where name = 'FK_ProjectPackage_Package')
Begin
	ALTER TABLE [Reliance].[ProjectPackage]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPackage_Package] FOREIGN KEY([PackageId]) REFERENCES [Reliance].[Package] ([Id])
End 
Go

If Not Exists(Select * From sys.indexes Where name = 'ProjectPackageIndex')
Begin
	CREATE UNIQUE NONCLUSTERED INDEX [ProjectPackageIndex] ON [Reliance].[ProjectPackage]
	(
		[ProjectId] ASC,
		[PackageId] ASC
	)
End
Go