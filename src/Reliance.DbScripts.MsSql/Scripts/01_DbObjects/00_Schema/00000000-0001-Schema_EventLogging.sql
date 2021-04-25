
If Not Exists(Select 1 From sys.schemas Where name = 'EventLogging')
Begin
	Execute('Create Schema EventLogging')
End
GO