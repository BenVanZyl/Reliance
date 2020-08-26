

If Not Exists(Select 1 From sys.tables Where name = 'Apps')
Begin
	Create Table DevOps.Apps
	(
		Id					bigint	Identity(1,1) NOT NULL,
		CreateDateTime		dateTime NOT NULL,
		ModifyDateTime		dateTime NOT NULL,
		Name				varchar(1024) NOT NULL,
		CONSTRAINT [PK_Apps] PRIMARY KEY CLUSTERED
		(
			[Id] ASC
		)
	)
End
GO

If Not Exists(select 1 from sys.indexes Where name = 'IX_DevOps_Apps_Name')
Begin
	CREATE NONCLUSTERED INDEX [IX_DevOps_Apps_Name] ON [DevOps].[Apps]
	(
		[Name]	ASC
	)
End
Go

