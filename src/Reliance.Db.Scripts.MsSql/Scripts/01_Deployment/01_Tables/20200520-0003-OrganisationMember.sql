
If Not Exists(Select 1 From sys.tables Where name = 'OrganisationMember')
Begin
	Create Table Info.OrganisationMember
	(
		Id					bigint	Identity(1,1) NOT NULL,
		CreatedOn		dateTime NOT NULL,
		ModifiedOn		dateTime NOT NULL,
		OrganisationId		bigint NOT NULL,
		Email				varchar(1024) NOT NULL,
		Name				nvarchar(512),
		IsActive			bit default 1,
		CONSTRAINT [PK_OrganisationMember] PRIMARY KEY CLUSTERED
		(
			[Id] ASC
		)
	)
End
GO

If Not Exists(select 1 from sys.indexes Where name = 'IX_OrganisationMember_Org')
Begin
	CREATE NONCLUSTERED INDEX [IX_OrganisationMember_Org] ON [Info].[OrganisationMember]
	(
		OrganisationId ASC
	)
End
Go


If Not Exists(select 1 from sys.indexes Where name = 'IX_OrganisationMember_Email')
Begin
	CREATE NONCLUSTERED INDEX [IX_OrganisationMember_Email] ON [Info].[OrganisationMember]
	(
		Email ASC
	)
End
Go