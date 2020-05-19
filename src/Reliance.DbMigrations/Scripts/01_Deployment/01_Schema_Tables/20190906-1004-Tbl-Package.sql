If Not Exists(Select 1 From sys.tables Where name = 'Package')
Begin
	Create Table Reliance.Package
	(
		Id					bigint	Identity(1,1) NOT NULL,
		CreateUserId		bigint NOT NULL,
		CreateDateTime		dateTime NOT NULL,
		ModifyUserId		bigint NOT NULL,
		ModifyDateTime		dateTime NOT NULL,
		Name				varchar(1024) NOT NULL,
		VersionMaster		int NOT NULL Default 0,
		VersionMinor		int NOT NULL Default 0,
		VersionPatch		int NOT NULL Default 0,
		VersionMinorPatch	int NOT NULL Default 0,
		FullVersion			varchar(255) NOT NULL,
		TargetFrameWork		varchar(255) NOT NULL,
		CONSTRAINT [PK_Package] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)
	)
End 
GO

If Not Exists(select 1 from sys.indexes Where name = 'IX_Package_Name_Version')
Begin
	CREATE NONCLUSTERED INDEX [IX_Package_Name_Version] ON [Reliance].[Package]
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
	CREATE NONCLUSTERED INDEX [IX_Package_Name] ON [Reliance].[Package]
	(
		Name			ASC
	)
End
Go