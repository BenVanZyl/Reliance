

If Not Exists(Select 1 From sys.tables Where name = 'Stages')
Begin
	Create Table DevOps.Stages
	(
		Id					bigint	Identity(1,1) NOT NULL,
		CreatedOn		dateTime NOT NULL,
		ModifiedOn		dateTime NOT NULL,
		OrganisationId	bigint NOT NULL,
		Name				varchar(1024) NOT NULL,
		OrderBy				bigint NOT NULL Default 0,
		CONSTRAINT [PK_Stages] PRIMARY KEY CLUSTERED
		(
			[Id] ASC
		)
	)
End
GO

If Not Exists(select 1 from sys.indexes Where name = 'IX_DevOps_Stages_Name')
Begin
	CREATE NONCLUSTERED INDEX [IX_DevOps_Stages_Name] ON [DevOps].[Stages]
	(
		[Name]	ASC
	)
End
Go

