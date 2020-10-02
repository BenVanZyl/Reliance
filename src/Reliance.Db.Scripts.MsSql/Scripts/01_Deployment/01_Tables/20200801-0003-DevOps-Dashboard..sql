

If Not Exists(Select 1 From sys.tables Where name = 'Dashboards')
Begin
	Create Table DevOps.Dashboards
	(
		Id					bigint	Identity(1,1) NOT NULL,
		CreatedOn		dateTime NOT NULL,
		ModifiedOn		dateTime NOT NULL,
		AppId				bigint NOT NULL,
		StageId				bigint NOT NULL,
		BadgeUrl			nvarchar(2048) NOT NULL,
		CONSTRAINT [PK_Dashboards] PRIMARY KEY CLUSTERED
		(
			[Id] ASC
		)
	)
End
GO

If Not Exists(select 1 from sys.indexes Where name = 'IX_DevOps_Dashboards_App_Stage')
Begin
	CREATE NONCLUSTERED INDEX [IX_DevOps_Dashboards_App_Stage] ON [DevOps].[Dashboards]
	(
		[AppId]	ASC,
		[StageId] ASC
	)
End
Go

