

If Not Exists(Select 1 From sys.tables Where name = 'Badge')
Begin
	Create Table DevOps.Badge
	(
		Id					bigint	Identity(1,1) NOT NULL,
		CreatedOn		dateTime NOT NULL,
		ModifiedOn		dateTime NOT NULL,
		AppId				bigint NOT NULL,
		StageId				bigint NOT NULL,
		BadgeUrl			nvarchar(2048) NOT NULL,
		CONSTRAINT [PK_Badge] PRIMARY KEY CLUSTERED
		(
			[Id] ASC
		)
	)
End
GO

If Not Exists(select 1 from sys.indexes Where name = 'IX_DevOps_Badge_App_Stage')
Begin
	CREATE NONCLUSTERED INDEX IX_DevOps_Badge_App_Stage ON [DevOps].Badge
	(
		[AppId]	ASC,
		[StageId] ASC
	)
End
Go

