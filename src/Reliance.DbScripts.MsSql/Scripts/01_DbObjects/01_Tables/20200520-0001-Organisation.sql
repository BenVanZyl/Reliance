
If Not Exists(Select 1 From sys.tables Where name = 'Organisation')
Begin
	Create Table Info.Organisation
	(
		Id					bigint	Identity(1,1) NOT NULL,
		CreatedOn		dateTime NOT NULL,
		ModifiedOn		dateTime NOT NULL,
		Name				nvarchar(1024) NOT NULL,
		MasterEmail			nvarchar(1024) NOT NULL,
		CONSTRAINT [PK_Organisation] PRIMARY KEY CLUSTERED
		(
			[Id] ASC
		)
	)
End
GO

If Not Exists(select 1 from sys.indexes Where name = 'IX_Organisation')
Begin
	CREATE NONCLUSTERED INDEX [IX_Organisation] ON [Info].[Organisation]
	(
		Name			ASC
	)
End
Go

