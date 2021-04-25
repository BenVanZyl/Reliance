/*
	adding info of Reliance into these tables as a starting point.
*/
Begin

-- Organisation
	Insert into Info.Organisation (CreatedOn, ModifiedOn, Name, MasterEmail)
		Select GetDate(), GetDate(), '2ManyMinds', 'systems@2manyminds.com' -- does not really exists
		Where Not Exists (Select 1 From Info.Organisation Where Name = '2ManyMinds')

	Declare @orgId bigint
	Select @orgId = Id From Info.Organisation Where Name = '2ManyMinds'

	Declare @data table(NameValue nvarchar(1024) NULL, OrderBy bigint NULL)

--Apps
	Insert Into @data(NameValue) Values
		('SnowStorm'),
		('Reliance')

	Insert into DevOps.Apps (CreatedOn, ModifiedOn, OrganisationId, Name)
		Select GetDate(), GetDate(), @orgId, d.NameValue
		From @data d
		Where Not Exists (Select 1 From DevOps.Apps Where Name = d.NameValue and OrganisationId = @orgId)

--Stages
	Delete From @data
	Insert Into @data(NameValue, OrderBy) Values
		('Build', 1),
		('Test', 2),
		('Production', 3)

	Insert into DevOps.Stages (CreatedOn, ModifiedOn, OrganisationId, Name, OrderBy)
		Select GetDate(), GetDate(), @orgId, d.NameValue, d.OrderBy
		From @data d
		Where Not Exists (Select 1 From DevOps.Stages Where Name = d.NameValue and OrganisationId = @orgId)

--Dashboard
	Declare @data2 table(AppName nvarchar(1024), StageName nvarchar(1024), BadgeUrl nvarchar(2024))
	Insert Into @data2 Values
		('SnowStorm', 'Build', ''),
		('SnowStorm', 'Test', ''),
		('SnowStorm', 'Production', ''),
		('Reliance', 'Build', ''),
		('Reliance', 'Test', ''),
		('Reliance', 'Production', '')

End