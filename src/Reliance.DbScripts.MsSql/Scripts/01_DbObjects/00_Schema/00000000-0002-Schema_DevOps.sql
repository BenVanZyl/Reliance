
If Not Exists(Select 1 From sys.schemas Where name = 'DevOps')
Begin
	Execute('Create Schema DevOps')
End
GO