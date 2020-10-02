
If Not Exists(Select 1 From sys.tables Where name = 'OrganisationKey')
Begin
	Create Table Info.OrganisationKey
	(
		Id					bigint	Identity(1,1) NOT NULL,
		CreatedOn		dateTime NOT NULL,
		ModifiedOn		dateTime NOT NULL,
		OrganisationId		bigint NOT NULL,
		PrivateKey			nvarchar(36) NOT NULL Default NewID(),
		Description			nvarchar(256) NULL,
		ExpiryDate			dateTime NOT NULL,
		CONSTRAINT [PK_OrganisationKey] PRIMARY KEY CLUSTERED
		(
			[Id] ASC
		)
	)
End
GO

If Not Exists(select 1 from sys.indexes Where name = 'IX_OrganisationKey')
Begin
	CREATE NONCLUSTERED INDEX [IX_OrganisationKey] ON [Info].[OrganisationKey]
	(
		OrganisationId ASC
	)
End
Go

