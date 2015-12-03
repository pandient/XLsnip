   Dim xl  
   Dim a
   Dim i
   Dim nm
   
    Set xl = CreateObject("Excel.Application")
	xl.Visible = false
	xl.DisplayAlerts = false

	For i = 1 To xl.Addins.Count
		Set a = xl.Addins.item(i)
		nm = UCase(a.Name)
		If nm = "HACKTHON.XLAM" Then
			if a.Installed = true then
				a.Installed = false
			End If
            set a = nothing
			Exit For
		End If
		set a = nothing
	Next
	
	xl.Quit
	Set xl = Nothing
 