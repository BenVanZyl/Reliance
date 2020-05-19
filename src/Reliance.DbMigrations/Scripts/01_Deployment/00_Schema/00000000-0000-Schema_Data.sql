
If Not Exists(Select 1 From sys.schemas Where name = 'Reliance')
Begin
	Execute('Create Schema Reliance')
End
GO