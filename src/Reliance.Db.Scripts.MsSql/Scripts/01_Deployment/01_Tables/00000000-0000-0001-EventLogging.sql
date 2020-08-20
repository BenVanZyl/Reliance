
If Not Exists(Select 1 From sys.tables Where name = 'Logs')
Begin

	CREATE TABLE [EventLogging].[Logs](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Message] [nvarchar](max) NULL,
		[MessageTemplate] [nvarchar](max) NULL,
		[Level] [nvarchar](max) NULL,
		[TimeStamp] [datetime] NULL,
		[Exception] [nvarchar](max) NULL,
		[Properties] [nvarchar](max) NULL,
	 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


End
GO

If Not Exists(select 1 from sys.indexes Where name = 'IX_Logs_Level')
Begin
	CREATE NONCLUSTERED INDEX [IX_Logs_Level] ON [EventLogging].[Logs]
	(
		[Level]
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