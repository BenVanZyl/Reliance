
If Not Exists(Select 1 From sys.schemas Where name = 'Info')
Begin
	Execute('Create Schema Info')
End
GO