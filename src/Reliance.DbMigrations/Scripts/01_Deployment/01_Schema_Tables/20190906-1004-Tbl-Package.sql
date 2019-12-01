If Not Exists(Select 1 From sys.tables Where name = 'Package')
Begin
	Create Table dbo.Package
	(
		Id					int	Identity(1,1) NOT NULL,
		Name				varchar(1024) NOT NULL,
		VersionMaster		int NOT NULL Default 0,
		VersionMinor		int NOT NULL Default 0,
		VersionPatch		int NOT NULL Default 0,
		TargetFrameWork		varchar(255) NOT NULL,
		CreateUserId		int NOT NULL,
		CreateDateTime		dateTime NOT NULL,
		ModifyUserId		int NOT NULL,
		ModifyDateTime		dateTime NOT NULL,
		CONSTRAINT [PK_Package] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)
	)
End 
GO

If Not Exists(select 1 from sys.indexes Where name = 'IX_Package_Name_Version')
Begin
	CREATE NONCLUSTERED INDEX [IX_Package_Name_Version] ON [dbo].[Package]
	(
		Name			ASC,
		VersionMaster	ASC,
		VersionMinor	ASC,
		VersionPatch	ASC
	)
End
Go

If Not Exists(select 1 from sys.indexes Where name = 'IX_Package_Name')
Begin
	CREATE NONCLUSTERED INDEX [IX_Package_Name] ON [dbo].[Package]
	(
		Name			ASC
	)
End
Go