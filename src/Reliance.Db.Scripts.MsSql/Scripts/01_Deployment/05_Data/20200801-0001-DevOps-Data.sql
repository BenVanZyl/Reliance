/*
	adding info of Reliance into these tables as a starting point.
*/
Begin
	Declare @data table(NameValue nvarchar(1024) NULL, OrderBy bigint NULL)

--Apps
	Insert Into @data(NameValue) Values
		('SnowStorm'),
		('Reliance')

	Insert into DevOps.Apps (CreateDateTime, ModifyDateTime, Name)
		Select GetDate(), GetDate(), d.NameValue
		From @data d
		Where Not Exists (Select 1 From DevOps.Apps Where Name = d.NameValue)

--Stages
	Delete From @data
	Insert Into @data(NameValue, OrderBy) Values
		('Build'),
		('Test'),
		('Production')

	Insert into DevOps.Stages (CreateDateTime, ModifyDateTime, Name)
		Select GetDate(), GetDate(), d.NameValue, d.OrderBy
		From @data d
		Where Not Exists (Select 1 From DevOps.Stages Where Name = d.NameValue)

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